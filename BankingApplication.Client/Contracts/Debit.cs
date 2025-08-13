using System.Runtime.Serialization;

namespace BankingApplication.Client.Contracts
{
    [DataContract]
    public record Debit
    {
        [DataMember]
        public decimal Amount { get; set; }
    }
}
