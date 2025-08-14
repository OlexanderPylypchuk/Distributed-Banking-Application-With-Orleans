using BankingApplication.Grains.Abstractions;
using BankingApplication.Grains.States;
using Orleans.Concurrency;

namespace BankingApplication.Grains.Grains
{
    [StatelessWorker]
    public class StatelessTransferProcessingGrain : Grain, IStatelessTransferProcessingGrain
    {
        private readonly IPersistentState<TransferState> _transferState;
        private readonly ITransactionClient _transactionClient;

        public StatelessTransferProcessingGrain([PersistentState("transfer", "TableStorage")]  IPersistentState<TransferState> transferState, ITransactionClient transactionClient)
        {
            _transferState = transferState;
            _transactionClient = transactionClient;
        }

        public async Task ProcessTransfer(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            var fromAccountGrain = this.GrainFactory.GetGrain<ICheckingAccountGrain>(fromAccountId);
            var toAccountGrain = this.GrainFactory.GetGrain<ICheckingAccountGrain>(toAccountId);

            await _transactionClient.RunTransaction(TransactionOption.Create, async () =>
            {
                await fromAccountGrain.Debit(amount);
                await toAccountGrain.Credit(amount);
            });

            _transferState.State.TransferCount += 1;

            await _transferState.WriteStateAsync();
        }
    }
}
