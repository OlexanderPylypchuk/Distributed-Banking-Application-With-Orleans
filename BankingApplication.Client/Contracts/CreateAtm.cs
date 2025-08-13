using System.Runtime.Serialization;

namespace BankingApplication.Client.Contracts
{
    [DataContract]
    public class CreateAtm
    {
        [DataMember]
        public decimal OpeningBalance { get; set; }
    }
}
