using System;
using System.Collections.Generic;
using System.Text;

namespace NovaBank.Domain.Events
{
    public record AccountOpened(Guid Id, string AccountNumber, decimal InitialBalance, DateTime OpenedAt);
}
