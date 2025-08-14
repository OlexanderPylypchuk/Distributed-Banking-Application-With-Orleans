using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.Grains.Events
{
    [GenerateSerializer]
    public record BalanceChangeEvent
    {
        [Id(0)]
        public Guid CheckingAccountId { get; set; }
        [Id(1)]
        public decimal Balance { get; set; }
    }
}
