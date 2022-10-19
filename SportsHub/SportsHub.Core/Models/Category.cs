namespace SportsHub.Domain.Models
{
    public class Category
    {
        public Category()
        {
            Articles = new HashSet<Article>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
