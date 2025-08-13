using BankingApplication.Grains.Abstractions;
using BankingApplication.Grains.States;

namespace BankingApplication.Grains.Grains
{
    public class AtmGrain : Grain, IAtmGrain
    {
        private readonly IPersistentState<AtmState> _atmState;
        public AtmGrain([PersistentState("atm", "TableStorage")] IPersistentState<AtmState> atmState)
        {
            _atmState = atmState;
        }
        public async Task Initialize(decimal openingBalance)
        {
            _atmState.State.Balance = openingBalance;
            _atmState.State.Id = this.GetGrainId().GetGuidKey();
        }

        public async Task Withdraw(Guid CheckingAccountId, decimal amount)
        {
            var checkingAccount = this.GrainFactory.GetGrain<ICheckingAccountGrain>(CheckingAccountId);

            await checkingAccount.Debit(amount);

            _atmState.State.Balance -= amount;

            await _atmState.WriteStateAsync();
        }
    }
}
