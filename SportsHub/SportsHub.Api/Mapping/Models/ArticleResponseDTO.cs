using SportsHub.Domain.Models;

namespace SportsHub.Api.Mapping.Models
{
    public class ArticleResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public string StateName { get; set; }
        public string CategoryName { get; set; }

        public ICollection<User> Authors { get; set; }
    }
}
