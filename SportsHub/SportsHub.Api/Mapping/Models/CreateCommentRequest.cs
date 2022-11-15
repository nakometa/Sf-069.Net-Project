namespace SportsHub.Api.Mapping.Models
{
    public class CreateCommentRequest
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public DateTime PostedOn { get; set; }
        public int ArticleId { get; set; }
        public int AuthorId { get; set; }
    }
}
