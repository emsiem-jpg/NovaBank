using NovaBank.Domain.Events;
using System;
using System.Collections.Generic;

namespace NovaBank.Domain
{
    public class Account
    {
        private readonly List<object> _uncommittedEvents = new();
        private const int AccountNumberLength = 26;
        private readonly List<Transaction> _transactions = new();

        public Guid Id { get; private set; }
        public decimal Balance { get; private set; }
        public string AccountNumber { get; private set; } = string.Empty;
        public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();

      
        private Account() { }

        public static Account Open(Guid id, string number, decimal initialBalance)
        {
            var account = new Account();
            var @event = new AccountOpened(id, number, initialBalance, DateTime.UtcNow);

            account.Apply(@event); 
            account._uncommittedEvents.Add(@event); 
            return account;
        }

        public void Apply(AccountOpened e)
        {
            Id = e.Id;
            AccountNumber = e.AccountNumber;
            Balance = e.InitialBalance;
        }

        public void Apply(MoneyDeposited e)
        {
            Balance += e.Amount;
            _transactions.Add(new Transaction(e.Amount, TransactionType.Deposit, e.Description));
        }

        public void Apply(MoneyWithdrawn e)
        {
            Balance -= e.Amount;
            _transactions.Add(new Transaction(e.Amount, TransactionType.Withdrawal, e.Description));
        }


        public void Deposit(decimal amount, string description = "Deposit")
        {
            if (amount <= 0) throw new ArgumentException("Amount must be positive");

            var @event = new MoneyDeposited(Id, amount, description, DateTime.UtcNow);
            Apply(@event);
            _uncommittedEvents.Add(@event);
        }

        public void Withdraw(decimal amount, string description = "Withdrawal")
        {
            if (amount <= 0) throw new ArgumentException("Amount must be positive");
            if (Balance < amount) throw new InvalidOperationException("Insufficient funds");

            var @event = new MoneyWithdrawn(Id, amount, description, DateTime.UtcNow);
            Apply(@event);
            _uncommittedEvents.Add(@event);
        }
        public IEnumerable<object> GetUncommittedEvents() => _uncommittedEvents;
        public void ClearUncommittedEvents() => _uncommittedEvents.Clear();
    }
}