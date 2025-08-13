using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.Grains.States
{
    [GenerateSerializer]
    public record AtmState
    {
        [Id(0)]
        public Guid Id { get; set; }
        [Id(1)]
        public decimal Balance { get; set; }
    }
}
