using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();  
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddHostedService<KafkaConsumer>();
            });
    }

    public class KafkaConsumer : IHostedService
    {
        private readonly IConsumer<string, string> _consumer;   

        public KafkaConsumer()
        {
            var config = new ConsumerConfig
            {
                GroupId = "demo-group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<string, string>(config)
               .SetKeyDeserializer(Deserializers.Utf8)
               .SetValueDeserializer(Deserializers.Utf8)
               .Build();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _consumer.Subscribe("demo");

            CancellationTokenSource token = new();

            while (true)
            {
                var result = _consumer.Consume(token.Token);

                if (result?.Message == null) continue;

                var article = JsonSerializer.Deserialize<CreateArticleDTO>(result.Message.Value);

                if (article == null) Console.WriteLine("Article is null");

                Console.WriteLine($"{article?.Title}");

                _consumer.Commit(result);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _consumer.Dispose();
            return Task.CompletedTask;
        }
    }

    public class CreateArticleDTO
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int CategoryId { get; set; }
    }
}