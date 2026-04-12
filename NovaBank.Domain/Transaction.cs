using System;
using System.Collections.Generic;
using System.Text;

namespace NovaBank.Domain
{
        public enum TransactionType
        {
            Deposit,
            Withdrawal,
        }
        public class Transaction
        {
            public Guid Id { get; private set; }
            public decimal Amount { get; private set; }

            public DateTime Date { get; private set; }

            public TransactionType Type { get; private set; }

            public string Description { get; private set; }

            public Transaction(decimal amount, TransactionType type, string description)
            {
                Id = Guid.NewGuid();
                Amount = amount;
                Date = DateTime.UtcNow;
                Type = type;
                Description = description;
            }
        }
    
}
