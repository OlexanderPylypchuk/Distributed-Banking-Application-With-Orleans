using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.Grains.Abstractions
{
    internal interface IAtmGrain:  IGrainWithGuidKey
    {
        public Task Withdraw(Guid CheckingAccountId, decimal amount);
    }
}
