using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.Grains.States
{
    [GenerateSerializer]
    public record CustomerState
    {
        [Id(0)]
        public Dictionary<Guid, decimal> CheckingAccountBalanceById { get; set; } = new();
    }
}
