using SportsHub.Domain.Models;

namespace SportsHub.Domain.Kafka
{
    public interface IKafkaProducer
    {
        public void ProduceArticle(Article output);
    }
}
