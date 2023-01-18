using Confluent.Kafka;
using Newtonsoft.Json;
using SportsHub.Domain.Kafka;
using SportsHub.Domain.Models;

namespace SportsHub.AppService.Kafka
{
    public class KafkaProducer : IKafkaProducer
    {
        public async Task ProduceArticle(Article output)
        {
            var articleResponseDTO = new KafkaArticleDTO
            {
                Id = output.Id,
                Title= output.Title,
                Content= output.Content,
                CreatedOn= output.CreatedOn,
                PostedOn= output.PostedOn
            };
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
            using var producer = new ProducerBuilder<Null, string>(config).Build();
            var response = await producer.ProduceAsync("article-topic",
                new Message<Null, string> { Value = JsonConvert.SerializeObject(articleResponseDTO) });
        }
    }
}
