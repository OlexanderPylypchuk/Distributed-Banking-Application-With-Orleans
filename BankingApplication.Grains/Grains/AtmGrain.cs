using BankingApplication.Grains.Abstractions;
using BankingApplication.Grains.States;
using Microsoft.Extensions.Logging;
using Orleans.Concurrency;
using Orleans.Transactions.Abstractions;

namespace BankingApplication.Grains.Grains
{
    [Reentrant]
    public class AtmGrain : Grain, IAtmGrain, IIncomingGrainCallFilter
    {
        private readonly ITransactionalState<AtmState> _atmTState;
        private readonly ILogger _logger;
        public AtmGrain([TransactionalState("atm")] ITransactionalState<AtmState> atmTState, ILogger logger)
        {
            _atmTState = atmTState;
            _logger = logger;
        }
        public async Task Initialize(decimal openingBalance)
        {
            await _atmTState.PerformUpdate(state =>
            {
                state.Balance = openingBalance;
                state.Id = this.GetGrainId().GetGuidKey();
            });
        }

        public async Task Invoke(IIncomingGrainCallContext context)
        {
            _logger.LogInformation($"Intercepted in {context.Grain}");

            await context.Invoke();
        }

        public async Task Withdraw(Guid CheckingAccountId, decimal amount)
        {
            var checkingAccount = this.GrainFactory.GetGrain<ICheckingAccountGrain>(CheckingAccountId);

            await _atmTState.PerformUpdate(state =>
            {
                state.Balance -= amount;
            });
        }
    }
}
