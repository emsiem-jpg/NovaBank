using System;
using System.Collections.Generic;
using System.Linq;

namespace NovaBank.Domain
{
    public class BankService
    {
        private readonly List<Account> _accounts = new List<Account>();

        public void RegisterAccount(Account account)
        {
            if (_accounts.Any(a => a.AccountNumber == account.AccountNumber))
                throw new InvalidOperationException("Account with this number already exists.");

            _accounts.Add(account);
        }

        public void PerformTransfer(string fromNumber, string toNumber, decimal amount)
        {
            var source = _accounts.FirstOrDefault(a => a.AccountNumber == fromNumber);
            var destination = _accounts.FirstOrDefault(a => a.AccountNumber == toNumber);

            if (source == null || destination == null)
                throw new ArgumentException("One or both account numbers are invalid.");

            source.Withdraw(amount, $"Transfer to {toNumber}");
            destination.Deposit(amount, $"Transfer from {fromNumber}");
        }

        public Account GetAccount(string accountNumber)
        {
            return _accounts.FirstOrDefault(a => a.AccountNumber == accountNumber)
                   ?? throw new KeyNotFoundException("Account not found.");
        }
    }
}