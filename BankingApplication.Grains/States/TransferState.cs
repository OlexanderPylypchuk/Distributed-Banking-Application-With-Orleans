using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.Grains.States
{
    [GenerateSerializer]
    public record TransferState
    {
        [Id(0)]
        public int TransferCount { get; set; }
    }
}
