namespace SportsHub.AppService.Authentication.Models.DTOs
{
    public class CreateCommentDTO
    {
        public string Content { get; set; }

        public int ArticleId { get; set; }

        public int AuthorId { get; set; }
    }
}
