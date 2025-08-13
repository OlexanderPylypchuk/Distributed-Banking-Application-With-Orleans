using BankingApplication.Grains.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.Grains.Grains
{
    public class CheckingAccountGrain : Grain, ICheckingAccountGrain
    {
        private decimal _balance;
        public async Task<decimal> GetBalance()
        {
            return _balance;
        }

        public async Task Initialize(decimal openingBalance)
        {
            _balance = openingBalance;
        }
    }
}
