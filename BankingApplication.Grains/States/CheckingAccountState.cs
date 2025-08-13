using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.Grains.States
{
    [GenerateSerializer]
    public record CheckingAccountState
    {
        [Id(0)]
        public Guid Accountid { get; set; }
        [Id(1)]
        public DateTime CreatedAtUtc { get; set; }
        [Id(2)]
        public string AccountType { get; set; }
        [Id(3)]
        public List<ReccuringPayment> ReccuringPayments { get; set; }
    }
}
