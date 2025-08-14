using System.Runtime.Serialization;

namespace BankingApplication.Client.Contracts
{
    [DataContract]
    public record TransferRecord
    {
        [DataMember]
        public Guid fromAccountId { get; set; }
        [DataMember] 
        public Guid toAccountId { get; set; }
        [DataMember]
        public decimal Amount { get; set; }
    }
}
