using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsHub.Domain.Models;

namespace SportsHub.DAL.Data.Configurations
{
    public class ArticleEntityTypeConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> article)
        {
            article.Property(x => x.Title)
                .IsRequired(true)
                .HasMaxLength(100)
                .IsUnicode(true);

            article.Property(x => x.Content)
                .IsRequired(true)
                .IsUnicode(true);

            article.Property(x => x.CreatedOn)
                .IsRequired(true)
                .HasDefaultValueSql("GETDATE()");

            article.Property(x => x.PostedOn)
                .IsRequired(false);

            article.Property(x => x.StateId)
                .IsRequired(true);

            article.Property(x => x.CategoryId)
                .IsRequired(true);
        }
    }
}
