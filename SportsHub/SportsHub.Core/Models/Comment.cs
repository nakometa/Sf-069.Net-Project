namespace SportsHub.Domain.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime PostedOn { get; set; }

        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }

        public int AuthorId { get; set; }
        public virtual User Author { get; set; }

        public virtual ICollection<CommentLike> CommentsLikes { get; set; }
    }
}
