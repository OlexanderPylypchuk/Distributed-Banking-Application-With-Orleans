using BankingApplication.Grains.Abstractions;
using BankingApplication.Grains.States;
using Orleans.Concurrency;
using Orleans.Transactions.Abstractions;

namespace BankingApplication.Grains.Grains
{
    [Reentrant]
    public class AtmGrain : Grain, IAtmGrain
    {
        private readonly ITransactionalState<AtmState> _atmTState;
        public AtmGrain([TransactionalState("atm")] ITransactionalState<AtmState> atmTState)
        {
            _atmTState = atmTState;
        }
        public async Task Initialize(decimal openingBalance)
        {
            await _atmTState.PerformUpdate(state =>
            {
                state.Balance = openingBalance;
                state.Id = this.GetGrainId().GetGuidKey();
            });
        }

        public async Task Withdraw(Guid CheckingAccountId, decimal amount)
        {
            var checkingAccount = this.GrainFactory.GetGrain<ICheckingAccountGrain>(CheckingAccountId);

            await _atmTState.PerformUpdate(state =>
            {
                state.Balance -= amount;
            });
        }
    }
}
