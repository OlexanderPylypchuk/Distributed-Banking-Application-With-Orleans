using BankingApplication.Grains.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.Grains.Grains
{
    public class AtmGrain : Grain, IAtmGrain
    {
        public Task Withdraw(Guid CheckingAccountId, decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}
