namespace SportsHub.Domain.Models
{
    public class Sport
    {
        public Sport()
        {
            Leagues = new HashSet<League>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<League> Leagues { get; set; }
    }
}
