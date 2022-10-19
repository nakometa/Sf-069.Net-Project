using SportsHub.Domain.Models;

namespace SportsHub.AppService.Authentication.Models.DTOs
{
    public class ArticleResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public string StateName { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<User> Authors { get; set; }
    }
}
