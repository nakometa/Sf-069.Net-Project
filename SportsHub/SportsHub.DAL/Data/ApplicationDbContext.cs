using Microsoft.EntityFrameworkCore;
using SportsHub.DAL.Data.Configurations;
using SportsHub.Domain.Models;

namespace SportsHub.DAL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new UserEntityTypeConfiguration().Configure(modelBuilder.Entity<User>());
            new RoleEntityTypeConfiguration().Configure(modelBuilder.Entity<Role>());
            new StateEntityTypeConfiguration().Configure(modelBuilder.Entity<State>());
            new ArticleEntityTypeConfiguration().Configure(modelBuilder.Entity<Article>());
            new CommentEntityTypeConfiguration().Configure(modelBuilder.Entity<Comment>());
            new CategoryEntityTypeConfiguration().Configure(modelBuilder.Entity<Category>());
        }
    }
}
