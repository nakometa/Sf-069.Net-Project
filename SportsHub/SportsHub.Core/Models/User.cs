namespace SportsHub.Domain.Models
{
    public class User
    {
        public User()
        {
            Articles = new HashSet<Article>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public byte[] ProfilePicture { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
