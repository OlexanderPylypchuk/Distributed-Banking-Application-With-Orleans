using System.Runtime.Serialization;

namespace BankingApplication.Client.Contracts
{
    [DataContract]
    public class CreateAccount
    {
        [DataMember]
        public decimal OpeningBalance { get; set; }
    }
}
