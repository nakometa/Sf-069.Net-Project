using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.Domain.Models;
using System.Text.Json;

namespace SportsHub.AppService.Services
{
    public class KafkaProducerService: IKafkaProducer
    {
        private readonly ILogger<KafkaProducerService> _logger;
        private readonly ProducerConfig _config;

        public KafkaProducerService(ILogger<KafkaProducerService> logger, IOptions<ProducerConfig> config)
        {
            _logger = logger;
            _config = config.Value;
        }

        public async Task StartAsync(CreateArticleDTO article)
        {
            _logger.LogInformation("The operation have started");

            using var producer = new ProducerBuilder<string, string>(_config)
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
                throw new Exception($"Could not produce {deliveryResult.Message}");
            }

            _logger.LogInformation("The result has been delivered");
        }
    }
}
