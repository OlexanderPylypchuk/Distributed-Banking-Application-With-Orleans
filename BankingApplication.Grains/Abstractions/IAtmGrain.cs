using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.Grains.Abstractions
{
    public interface IAtmGrain:  IGrainWithGuidKey
    {
        public Task Initialize(decimal openingBalance);
        public Task Withdraw(Guid CheckingAccountId, decimal amount);
    }
}
