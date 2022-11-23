using SportsHub.Domain.Models;

namespace SportsHub.Api.Mapping.Models
{
    public class SportResponseDTO
    {
        public int Id { get; set;  }
        public string Name { get; set;  }
        public string Description { get; set; }

        public ICollection<League> Leagues { get; set; }

    }
}
