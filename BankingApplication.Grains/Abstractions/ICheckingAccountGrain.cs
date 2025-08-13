namespace BankingApplication.Grains.Abstractions
{
    public interface ICheckingAccountGrain : IGrainWithGuidKey
    {
        Task Initialize(decimal openingBalance);
        Task<decimal> GetBalance();
        Task Debit(decimal amount);
        Task Credit(decimal amount);
        Task AddReccuringPayment(Guid id, decimal amount, int reccursEveryMinutes);
    }
}
