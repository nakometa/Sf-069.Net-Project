using Confluent.Kafka;
using Newtonsoft.Json;

var config = new ConsumerConfig
{
    GroupId = "article-consumer-group",
    BootstrapServers = "localhost:9092",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

var consumer = new ConsumerBuilder<Null, string>(config).Build();
consumer.Subscribe("article-topic");

CancellationTokenSource token = new();

try
{
    while (true)
    {
        var response = consumer.Consume(token.Token);
        if (response != null)
        {
            var output = JsonConvert.DeserializeObject(response.Message.Value);
            Console.WriteLine(output);
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}