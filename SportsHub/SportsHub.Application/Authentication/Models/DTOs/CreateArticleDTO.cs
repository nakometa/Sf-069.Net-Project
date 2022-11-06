namespace SportsHub.AppService.Authentication.Models.DTOs
{
    public class CreateArticleDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public byte[] ArticlePicture { get; set; }
        public int CategoryId { get; set; }
    }
}