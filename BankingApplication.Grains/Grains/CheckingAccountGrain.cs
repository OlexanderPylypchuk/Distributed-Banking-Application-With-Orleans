using BankingApplication.Grains.Abstractions;
using BankingApplication.Grains.Events;
using BankingApplication.Grains.States;
using Orleans.Concurrency;
using Orleans.Transactions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.Grains.Grains
{
    [Reentrant]
    public class CheckingAccountGrain : Grain, ICheckingAccountGrain, IRemindable
    {
        private readonly ITransactionClient _transactionClient;
        private readonly ITransactionalState<BalanceState> _balanceState;
        private readonly IPersistentState<CheckingAccountState> _checkingAccountState;
        public CheckingAccountGrain([TransactionalState("balance")] ITransactionalState<BalanceState> balanceState,
            [PersistentState("checkingAccount", "BlobStorage")] IPersistentState<CheckingAccountState> checkingAccountState,
            ITransactionClient transactionClient)
        {
            _balanceState = balanceState;
            _checkingAccountState = checkingAccountState;
            _transactionClient = transactionClient;
        }

        public async Task AddReccuringPayment(Guid id, decimal amount, int reccursEveryMinutes)
        {
            _checkingAccountState.State.ReccuringPayments.Add(new ReccuringPayment{
                PaymentId = id,
                Amount = amount,
                OccursEveryMinutes = reccursEveryMinutes
            });

            await _checkingAccountState.WriteStateAsync();

            await this.RegisterOrUpdateReminder($"ReccurintPayment:::{id}", 
                TimeSpan.FromMinutes(reccursEveryMinutes), TimeSpan.FromMinutes(reccursEveryMinutes));
        }

        public async Task Credit(decimal amount)
        {
            await _balanceState.PerformUpdate(state =>
            {
                state.Balance += amount;
            });

            await TriggerChangeBalanceEvent();
        }

        public async Task Debit(decimal amount)
        {
            await _balanceState.PerformUpdate(state =>
            {
                state.Balance -= amount;
            });

            await TriggerChangeBalanceEvent();
        }
        private async Task TriggerChangeBalanceEvent()
        {
            var streamProvider = this.GetStreamProvider("streamProvider");

            var streamId = StreamId.Create("BalanceStream", this.GetGrainId().GetGuidKey());

            var stream = streamProvider.GetStream<BalanceChangeEvent>(streamId);

            await stream.OnNextAsync(new BalanceChangeEvent()
            {
                CheckingAccountId = this.GetGrainId().GetGuidKey(),
                Balance = await GetBalance()
            });
        }

        public async Task<decimal> GetBalance()
        {
            return await _balanceState.PerformRead(state => state.Balance);
        }

        public async Task Initialize(decimal openingBalance)
        {
            var balanceUpdate = _balanceState.PerformUpdate(state =>
            {
                state.Balance = openingBalance;
            });

            _checkingAccountState.State.CreatedAtUtc = DateTime.UtcNow;
            _checkingAccountState.State.AccountType = "Default";
            _checkingAccountState.State.Accountid = this.GetGrainId().GetGuidKey();

            var checkingAccountUpdate = _checkingAccountState.WriteStateAsync();

            await Task.WhenAll(balanceUpdate, checkingAccountUpdate);
        }

        public async Task ReceiveReminder(string reminderName, TickStatus status)
        {
            if (reminderName.StartsWith("ReccurintPayment"))
            {
                var reccuringPaymentId = Guid.Parse(reminderName.Split(":::").Last());

                var reccuringPayment = _checkingAccountState.State.ReccuringPayments.Where(p => p.PaymentId == reccuringPaymentId).Single();

                await _transactionClient.RunTransaction(transactionOption: TransactionOption.Create, async () =>
                {
                    await Debit(reccuringPayment.Amount);
                });
            }
        }
    }
}
