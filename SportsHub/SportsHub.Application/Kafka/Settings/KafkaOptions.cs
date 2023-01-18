namespace SportsHub.AppService.Kafka.Settings
{
    public class KafkaOptions
    {
        public const string Settings = "KafkaSettings";

        public string BootstrapServers { get; set; }
        public string Topic { get; set; }
    }
}
