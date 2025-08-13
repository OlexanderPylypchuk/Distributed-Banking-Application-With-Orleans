using BankingApplication.Grains.Abstractions;
using BankingApplication.Grains.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.Grains.Grains
{
    public class CheckingAccountGrain : Grain, ICheckingAccountGrain
    {
        private readonly IPersistentState<BalanceState> _balanceState;
        private readonly IPersistentState<CheckingAccountState> _checkingAccountState;
        public CheckingAccountGrain([PersistentState("balance", "TableStorage")]IPersistentState<BalanceState> balanceState,
            [PersistentState("checkingAccount", "BlobStorage")] IPersistentState<CheckingAccountState> checkingAccountState)
        {
            _balanceState = balanceState;
            _checkingAccountState = checkingAccountState;
        }
        public async Task Credit(decimal amount)
        {
            var currentBalance = _balanceState.State.Balance;
            
            var newstate = currentBalance + amount;

            _balanceState.State.Balance = newstate;

            await _balanceState.WriteStateAsync();
        }

        public async Task Debit(decimal amount)
        {
            var currentBalance = _balanceState.State.Balance;

            var newstate = currentBalance - amount;

            _balanceState.State.Balance = newstate;

            await _balanceState.WriteStateAsync();
        }

        public async Task<decimal> GetBalance()
        {
            return _balanceState.State.Balance;
        }

        public async Task Initialize(decimal openingBalance)
        {
            _checkingAccountState.State.CreatedAtUtc = DateTime.UtcNow;
            _checkingAccountState.State.AccountType = "Default";
            _checkingAccountState.State.Accountid = this.GetGrainId().GetGuidKey();

            _balanceState.State.Balance = openingBalance;
        }
    }
}
