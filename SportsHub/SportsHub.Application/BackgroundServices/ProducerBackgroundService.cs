using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SportsHub.AppService.Authentication.Models.DTOs;
using System.Text.Json;

namespace SportsHub.AppService.BackgroundServices
{
    public class ProducerBackgroundService : BackgroundService
    {
        private readonly IKafkaServiceChannel _channel;
        private readonly ILogger<ProducerBackgroundService> _logger;
        private readonly ProducerConfig _producerConfig;

        public ProducerBackgroundService(ILogger<ProducerBackgroundService> logger, IKafkaServiceChannel channel, IOptions<ProducerConfig> config)
        {
            _producerConfig = config.Value;
            _logger = logger;
            _channel = channel;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _channel.WaitToReadAsync(stoppingToken);

                if(!_channel.TryRead(out var article, stoppingToken))
                {
                    _logger.LogError("error in the channel stream");
                }

                _logger.LogInformation("everything worked well in the channel");

                if (article is not null)
                {
                    await ProduceToKafkaAsync(article);
                }
            }
        }

        private async Task ProduceToKafkaAsync(CreateArticleDTO article)
        {
            _logger.LogInformation("The producing to kafka have started");

            using var producer = new ProducerBuilder<string, string>(_producerConfig)
                .SetKeySerializer(Serializers.Utf8)
                .SetValueSerializer(Serializers.Utf8)
                .Build();

            var message = new Message<string, string>()
            {
                Key = Guid.NewGuid().ToString(),
                Value = JsonSerializer.Serialize(article)
            };

            var deliveryResult = await producer.ProduceAsync("demo", message);

            if (deliveryResult.Status == PersistenceStatus.NotPersisted)
            {
                _logger.LogError($"Could not produce {deliveryResult.Message}");
            }

            _logger.LogInformation("The result has been delivered");
        }
    }
}
