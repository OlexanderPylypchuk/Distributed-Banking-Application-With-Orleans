using BankingApplication.Client.Contracts;
using BankingApplication.Grains.Abstractions;
using BankingApplication.Grains.Grains;
using Orleans.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseOrleansClient((context, client) =>
{
    client.UseAzureStorageClustering(options =>
    {
        options.TableServiceClient = new Azure.Data.Tables.TableServiceClient("UseDevelopmentStorage=true;");
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

app.MapPost("checkingaccount/{checkingAccountId}/debit", async (Guid checkingAccountId, IClusterClient client, Debit debit) =>
{
    var checkinAccountGrain = client.GetGrain<ICheckingAccountGrain>(checkingAccountId);

    await checkinAccountGrain.Debit(debit.Amount);

    return TypedResults.NoContent();
});

app.MapPost("checkingaccount/{checkingAccountId}/credit", async (Guid checkingAccountId, IClusterClient client, Credit credit) =>
{
    var checkinAccountGrain = client.GetGrain<ICheckingAccountGrain>(checkingAccountId);

    await checkinAccountGrain.Credit(credit.Amount);

    return TypedResults.NoContent();
});

app.MapPost("checkingaccount/{checkingAccountId}/reccuringPayment", async (Guid checkingAccountId, IClusterClient client, CreateReccuringPayment createReccuringPayment) =>
{
    var checkinAccountGrain = client.GetGrain<ICheckingAccountGrain>(checkingAccountId);

    await checkinAccountGrain.AddReccuringPayment(createReccuringPayment.PaymentId, createReccuringPayment.Amount, createReccuringPayment.ReccursEveryMinutes);

    return TypedResults.NoContent();
});

app.MapPost("atm", async (IClusterClient client, CreateAtm createAtm) =>
{
    var atmId = Guid.NewGuid();

    var atmGrain = client.GetGrain<IAtmGrain>(atmId);

    await atmGrain.Initialize(createAtm.OpeningBalance);

    return TypedResults.Created($"atm/{atmId}");
});

app.MapPost("atm/{atmId}/credit", async (Guid atmId, IClusterClient client, AtmWithdrawl withdrawl) =>
{
    var atmGrain = client.GetGrain<IAtmGrain>(atmId);

    await atmGrain.Withdraw(withdrawl.CheckingAccountId, withdrawl.Amount);

    return TypedResults.NoContent();
});



app.Run();


