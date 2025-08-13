using System.Runtime.Serialization;

namespace BankingApplication.Client.Contracts
{
    [DataContract]
    public record AtmWithdrawl
    {
        [DataMember]
        public Guid CheckingAccountId { get; set; }
        [DataMember]
        public decimal Amount  { get; set; }
    }
}
