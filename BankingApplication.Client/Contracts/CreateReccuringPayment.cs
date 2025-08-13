using System.Runtime.Serialization;

namespace BankingApplication.Client.Contracts
{
    [DataContract]
    public record CreateReccuringPayment
    {
        [DataMember]
        public Guid PaymentId { get; set; }
        [DataMember]
        public decimal Amount { get; set; }
        [DataMember]
        public int ReccursEveryMinutes { get; set; }

    }
}
