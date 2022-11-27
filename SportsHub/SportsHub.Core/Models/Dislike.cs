namespace SportsHub.Domain.Models
{
    public class Dislike
    {
        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
