using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SportsHub.AppService.Authentication.Models.Options;
using SportsHub.AppService.Kafka.Settings;
using SportsHub.Domain.Kafka;
using SportsHub.Domain.Models;

namespace SportsHub.AppService.Kafka
{
    public class KafkaProducer : IKafkaProducer
    {
        private KafkaOptions _options;

        public KafkaProducer(IOptions<KafkaOptions> options)
        {
            _options = options.Value;
        }

        public Task ProduceArticle(Article output)
        {
            Task.Run(() => SendMessage(output));
            return Task.CompletedTask;
        }

        private async Task SendMessage(Article article)
        {
            var articleResponseDTO = new KafkaArticleDTO
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                CreatedOn = article.CreatedOn,
                PostedOn = article.PostedOn
            };
            var config = new ProducerConfig { BootstrapServers = _options.BootstrapServers };
            using var producer = new ProducerBuilder<Null, string>(config).Build();
            var response = await producer.ProduceAsync(_options.Topic,
                new Message<Null, string> { Value = JsonConvert.SerializeObject(articleResponseDTO) });

            Thread.Sleep(100000);
        }
    }
}
