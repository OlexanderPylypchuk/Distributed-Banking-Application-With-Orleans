using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.Grains.States
{
    [GenerateSerializer]
    public record ReccuringPayment
    {
        [Id(0)]
        public Guid PaymentId { get; set; }
        [Id(1)]
        public decimal Amount { get; set; }
        [Id(2)]
        public int OccursEveryMinutes { get; set; }
    }
}
