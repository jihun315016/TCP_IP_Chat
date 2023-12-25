using ChatService.Server;
using System.Reflection.Emit;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();

        // ����� ���� Ŭ���� ������ ����(�Ķ���Ͱ� �ִ� ���)
        //services.AddSingleton<Logging>
        //(
        //    serviceProvider =>
        //    new Logging
        //    (
        //        "logs",
        //        DateTime.Now.ToString("yyyyMMdd"),
        //        Level.All,
        //        30
        //    )
        //);

        // ����� ���� Ŭ���� ������ ����(�Ķ���Ͱ� ���� ���)
        //services.AddSingleton<Logging>();
    })
    .Build();

await host.RunAsync();
