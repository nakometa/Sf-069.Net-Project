using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace KafkaConsumer
{
    public class Consumer : BackgroundService
    {
        private readonly ILogger<Consumer> _logger;
        private readonly ConsumerOptions.ConsumerOptions _options;

        public Consumer(ILogger<Consumer> logger, ConsumerOptions.ConsumerOptions options)
        {
            _logger = logger;
            _options = options;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                GroupId = _options.GroupId,
                BootstrapServers= _options.BootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            var consumer = new ConsumerBuilder<Null, string>(config).Build();
            consumer.Subscribe(_options.Topic);

            CancellationTokenSource token = new();

            while (!stoppingToken.IsCancellationRequested)
            {
                var output = JsonConvert.DeserializeObject(consumer.Consume(token.Token).Message.Value);

                _logger.LogInformation("{article}", output);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
