namespace SportsHub.AppService.Kafka
{
    public class KafkaArticleDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? PostedOn { get; set; }
    }
}
