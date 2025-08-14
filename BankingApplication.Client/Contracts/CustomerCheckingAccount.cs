using System.Runtime.Serialization;

namespace BankingApplication.Client.Contracts
{
    [DataContract]
    public class CustomerCheckingAccount
    {
        [DataMember]
        public Guid AccountId { get; set; }
    }
}
