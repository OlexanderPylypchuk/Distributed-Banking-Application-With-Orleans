using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.Grains.Abstractions
{
    public interface IStatelessTransferProcessingGrain: IGrainWithIntegerKey
    {
        Task ProcessTransfer(Guid fromAccountId, Guid toAccountId, decimal amount);
    }
}
