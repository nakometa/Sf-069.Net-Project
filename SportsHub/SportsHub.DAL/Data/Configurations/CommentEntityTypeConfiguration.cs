using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsHub.DAL.Data.Configurations.Constants;
using SportsHub.Domain.Models;

namespace SportsHub.DAL.Data.Configurations
{
    public class CommentEntityTypeConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> comment)
        {
            comment.Property(x => x.Content)
                .IsRequired(true)
                .HasMaxLength(ConfigurationConstants.CommentConstants.CommentContentMaxLength)
                .IsUnicode(true);

            comment.Property(x => x.PostedOn)
                .IsRequired(true)
                .HasDefaultValueSql("now()");

            comment.Property(x => x.AuthorId)
                .IsRequired(true);

            comment.Property(x => x.ArticleId)
                .IsRequired(true);

            comment.HasOne(x => x.Author)
                .WithMany(x => x.Comments);

            comment.HasOne(x => x.Article)
                .WithMany(x => x.Comments);
        }
    }
}
