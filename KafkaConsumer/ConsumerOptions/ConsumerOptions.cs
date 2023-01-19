using Confluent.Kafka;

namespace KafkaConsumer.ConsumerOptions
{
    public class ConsumerOptions
    {
        public string GroupId { get; set; }
        public string BootstrapServers { get; set; }
        public string Topic { get; set; }
    }
}
