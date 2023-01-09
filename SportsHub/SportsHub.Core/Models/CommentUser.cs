namespace SportsHub.Domain.Models
{
    public abstract class CommentUser
    {
        public int CommentId { get; set; }
        public virtual Comment Comment { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
