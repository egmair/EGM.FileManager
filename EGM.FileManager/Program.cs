using EGM.FileManager;
using EGM.FileManager.Core.Extensions;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddFileManagement(opts =>
        {
            // TODO: Configure options.
        });
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
