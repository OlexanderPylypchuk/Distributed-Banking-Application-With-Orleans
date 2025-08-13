using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.Grains.Abstractions
{
    public interface IAtmGrain:  IGrainWithGuidKey
    {
        [Transaction(TransactionOption.Create)]
        public Task Initialize(decimal openingBalance);
        [Transaction(TransactionOption.CreateOrJoin)]
        public Task Withdraw(Guid CheckingAccountId, decimal amount);
    }
}
