namespace NovaBank.Domain.Infrastructure;

using Marten;
using NovaBank.Domain.Events;

public static class MartenConfig
{
    public static IDocumentStore GetStore(string connectionString)
    {
        return DocumentStore.For(opts =>
        {
            opts.Connection(connectionString);      //Connecting to the database

            opts.Events.AddEventType(typeof(AccountOpened));
            opts.Events.AddEventType(typeof(MoneyDeposited));       //Events registration
            opts.Events.AddEventType(typeof(MoneyWithdrawn));
        });
    }
}