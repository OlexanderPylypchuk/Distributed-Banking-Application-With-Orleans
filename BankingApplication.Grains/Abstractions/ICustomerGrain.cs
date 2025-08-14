using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.Grains.Abstractions
{
    public interface ICustomerGrain: IGrainWithGuidKey
    {
        public Task AddCheckingAccount(Guid accountId);
        public Task<decimal> GetNetWorth();
    }
}
