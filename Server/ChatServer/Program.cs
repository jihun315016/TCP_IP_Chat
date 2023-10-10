using ChatServer;
using log4net.Core;
using System.Configuration;
using System.Runtime.CompilerServices;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddSingleton<Logging>
        (
            serviceProvider => 
            new Logging
            (
                DateTime.Now.ToString("yyyyMMdd"),
                Level.All, 
                30
            )
        );
        // services.AddSingleton<Logging>();
    })
    .Build();

await host.RunAsync();
