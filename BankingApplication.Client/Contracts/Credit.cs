using System.Runtime.Serialization;

namespace BankingApplication.Client.Contracts
{
    [DataContract]
    public record Credit
    {
        [DataMember]
        public decimal Amount { get; set; }
    }
}
