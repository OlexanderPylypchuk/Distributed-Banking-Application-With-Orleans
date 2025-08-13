using BankingApplication.Client.Contracts;
using BankingApplication.Grains.Abstractions;
using Orleans.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseOrleansClient((context, client) =>
{
    client.UseAzureStorageClustering(options =>
    {
        options.TableServiceClient = new Azure.Data.Tables.TableServiceClient("UseDevelopementStorage=true;");
    });

    client.Configure<ClusterOptions>(options =>
    {
        options.ClusterId = "Cluster";
        options.ServiceId = "Service";
    });
});


var app = builder.Build();

app.MapGet("checkingaccount/{checkingAccountId}/balance", async (Guid checkingAccountId, IClusterClient client) =>
{
    var checkinAccountGrain = client.GetGrain<ICheckingAccountGrain>(checkingAccountId);

    var balance = await checkinAccountGrain.GetBalance();

    return TypedResults.Ok(balance);
});

app.MapPost("checkingaccount", async (IClusterClient client, CreateAccount createAccount) =>
{
    var checkingAccountId = Guid.NewGuid();

    var checkinAccountGrain = client.GetGrain<ICheckingAccountGrain>(checkingAccountId);

    await checkinAccountGrain.Initialize(createAccount.OpeningBalance);

    return TypedResults.Created($"checkingaccount/{checkingAccountId}");
});
app.Run();
