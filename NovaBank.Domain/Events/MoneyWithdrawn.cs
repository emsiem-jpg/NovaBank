using System;
using System.Collections.Generic;
using System.Text;

namespace NovaBank.Domain.Events
{
    public record MoneyWithdrawn(Guid Id, decimal Amount, string Description, DateTime OccurredAt);
}
