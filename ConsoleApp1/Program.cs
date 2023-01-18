using Confluent.Kafka;
using System.Text.Json;


namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            var config = new ConsumerConfig
            {
                GroupId = "demo-group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<string, string>(config)
                .SetKeyDeserializer(Deserializers.Utf8)
                .SetValueDeserializer(Deserializers.Utf8)
                .Build();

            consumer.Subscribe("demo");

            CancellationTokenSource token = new();

            while (true)
            {
                var result = consumer.Consume(token.Token);

                if (result?.Message == null) continue;

               var article = JsonSerializer.Deserialize<CreateArticleDTO>(result.Message.Value);

                if (article == null) Console.WriteLine("Article is null");

                Console.WriteLine($"{article?.Title}");

                consumer.Commit(result);
            }
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