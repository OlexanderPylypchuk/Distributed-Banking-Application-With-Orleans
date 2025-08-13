using Microsoft.Extensions.Hosting;
using Orleans.Configuration;

await Host.CreateDefaultBuilder(args)
    .UseOrleans(siloBuilder =>
    {
        siloBuilder.UseAzureStorageClustering(options =>
        {
            options.TableServiceClient = new Azure.Data.Tables.TableServiceClient("UseDevelopementStorage=true;");
        });

        siloBuilder.Configure<ClusterOptions>(options =>
        {
            options.ClusterId = "Cluster";
            options.ServiceId = "Service";
        });

        //siloBuilder.Configure<GrainCollectionOptions>(options =>
        //{
        //    options.CollectionQuantum = TimeSpan.FromSeconds(20);

        //    options.CollectionAge = TimeSpan.FromSeconds(20);
        //});
    }).RunConsoleAsync();
