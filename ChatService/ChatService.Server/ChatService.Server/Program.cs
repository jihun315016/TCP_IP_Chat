using ChatService.Server;
using System.Reflection.Emit;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();

        // 사용자 정의 클래스 의존성 주입(파라미터가 있는 경우)
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

        // 사용자 정의 클래스 의존성 주입(파라미터가 없는 경우)
        //services.AddSingleton<Logging>();
    })
    .Build();

await host.RunAsync();
