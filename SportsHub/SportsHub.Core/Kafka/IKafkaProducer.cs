using SportsHub.Domain.Models;

namespace SportsHub.Domain.Kafka
{
    public interface IKafkaProducer
    {
        public Task ProduceArticle(Article output);
    }
}
