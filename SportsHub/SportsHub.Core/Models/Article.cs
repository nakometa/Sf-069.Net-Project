namespace SportsHub.Domain.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? PostedOn { get; set; }
        public byte[] ArticlePicture { get; set; }

        public int StateId { get; set; }
        public virtual State State { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<User> Authors { get; set; }
    }
}
