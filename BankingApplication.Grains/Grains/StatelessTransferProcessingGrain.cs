using BankingApplication.Grains.Abstractions;
using BankingApplication.Grains.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.Grains.Grains
{
    public class StatelessTransferProcessingGrain : Grain, IStatelessTransferProcessingGrain
    {
        public StatelessTransferProcessingGrain([PersistentState("transfer", "TableStorage")]  IPersistentState<TransferState> transferState)
        {
            
        }
        public Task ProcessTransfer(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}
