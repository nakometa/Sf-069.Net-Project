namespace SportsHub.Domain.Models
{
    public class CommentLike
    {
        public int CommentId { get; set; }
        public virtual Comment Comment { get; set; }
        public bool IsLike { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
