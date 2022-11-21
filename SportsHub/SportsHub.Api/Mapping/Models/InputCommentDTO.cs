namespace SportsHub.Api.Mapping.Models
{
    public class InputCommentDTO
    {
        public string Content { get; set; }

        public int ArticleId { get; set; }

        public int AuthorId { get; set; }
    }
}
