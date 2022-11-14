using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsHub.DAL.Data.Configurations.Constants;
using SportsHub.Domain.Models;

namespace SportsHub.DAL.Data.Configurations
{
    public class ArticleEntityTypeConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> article)
        {
            article
                .HasIndex(x => x.Title)
                .IsUnique();

            article.Property(x => x.Title)
                .IsRequired(true)
                .HasMaxLength(ConfigurationConstants.ArticleTitleMaxLength)
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

            article.HasMany(x => x.Authors)
                .WithMany(x => x.Articles);

            article.HasMany(x => x.Comments)
                .WithOne(x => x.Article)
                .HasForeignKey(x => x.ArticleId);

            article.HasOne(x => x.Category)
                .WithMany(x => x.Articles);

            article.HasOne(x => x.State)
                .WithMany(x => x.Articles);

            article.Property(x => x.ArticlePicture)
                .IsRequired(false);
        }
    }
}
