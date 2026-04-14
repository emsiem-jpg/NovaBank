using System;
using System.Collections.Generic;

namespace NovaBank.Domain
{
    public class Account
    {
        private const int AccountNumberLength = 10;
        private readonly List<Transaction> _transactions;

        public Guid Id { get; private set; }
        public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();
        public decimal Balance { get; private set; }

        public string AccountNumber
        {
            get;
            private set
            {
                if (value.Length != AccountNumberLength)
                {
                    throw new ArgumentException($"Account number must be {AccountNumberLength} characters long.");
                }
                field = value;
            }
        }

        public Account(string accountNumber, decimal initialBalance)
        {
            Id = Guid.NewGuid();
            AccountNumber = accountNumber;
            _transactions = new List<Transaction>();

            if (initialBalance > 0)
            {
                Deposit(initialBalance, "Initial balance");
            }
        }

        public void Deposit(decimal amount, string description = "Deposit")
        {
            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be greater than zero.");

            Balance += amount;
            _transactions.Add(new Transaction(amount, TransactionType.Deposit, description));
        }

        public void Withdraw(decimal amount, string description = "Withdrawal")
        {
            if (amount <= 0)
                throw new ArgumentException("Withdrawal amount must be greater than zero.");
            if (amount > Balance)
                throw new InvalidOperationException("Insufficient funds.");

            Balance -= amount;
            _transactions.Add(new Transaction(amount, TransactionType.Withdrawal, description));
        }
    }
}