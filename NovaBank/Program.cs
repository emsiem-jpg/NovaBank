using Marten;
using NovaBank.Domain;
using NovaBank.Domain.Infrastructure;

var connectionString = "Host=localhost;Port=5432;Database=novabank;Username=admin;Password=password123";
using var store = MartenConfig.GetStore(connectionString);
using var session = store.LightweightSession();


var accountId = Guid.NewGuid();
var account = Account.Open(accountId, "PL00112233445566778899", 500m);


account.Deposit(200m, "Prezent od babci");
account.Withdraw(100m, "Zakupy w Żabce");


session.Events.StartStream(account.Id, account.GetUncommittedEvents());
await session.SaveChangesAsync();

Console.WriteLine($"Konto zapisane. Saldo końcowe: {account.Balance}");


var accountFromDb = await session.Events.AggregateStreamAsync<Account>(accountId);
Console.WriteLine($"Odczytano z bazy. Saldo: {accountFromDb?.Balance}");