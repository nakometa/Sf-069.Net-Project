using KafkaConsumer;
using KafkaConsumer.ConsumerOptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Consumer>();
        IConfiguration configuration = hostContext.Configuration;
        ConsumerOptions options = configuration.GetSection("Kafka").Get<ConsumerOptions>();
        services.AddSingleton(options);
    })
    .Build();

await host.RunAsync();