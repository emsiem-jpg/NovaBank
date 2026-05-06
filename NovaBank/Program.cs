using Marten;
using NovaBank.Domain;
using NovaBank.Domain.Infrastructure;
using System.Security.Principal;

var connectionString = "Host=localhost;Port=5432;Database=novabank;Username=admin;Password=password123";

using var store = MartenConfig.GetStore(connectionString);

using var session = store.LightweightSession();


var accountId = Guid.NewGuid();
var accountNumber = "1234567890";

session.Events.StartStream<Account>(accountId,
    new NovaBank.Domain.Events.AccountOpened(accountId, accountNumber, 100m, DateTime.UtcNow)
);

await session.SaveChangesAsync();

Console.WriteLine("Works.");