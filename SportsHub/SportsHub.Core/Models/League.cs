namespace SportsHub.Domain.Models
{
    public class League
    {
        public League()
        {
            Teams = new HashSet<Team>();
            SubLeagues = new HashSet<SubLeague>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int SportId { get; set; }
        public virtual Sport Sport { get; set; }

        public virtual ICollection<Team> Teams { get; set; }
        public virtual ICollection<SubLeague> SubLeagues { get; set; }
    }
}
