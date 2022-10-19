using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsHub.Domain.Models;

namespace SportsHub.DAL.Data.Configurations
{
    public class CommentEntityTypeConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> comment)
        {
            comment.Property(x => x.Content)
                .IsRequired(true)
                .HasMaxLength(450)
                .IsUnicode(false);

            comment.Property(x => x.PostedOn)
                .IsRequired(true)
                .HasDefaultValueSql("GETDATE()");

            comment.Property(x => x.AuthorId)
                .IsRequired(true);

            comment.Property(x => x.ArticleId)
                .IsRequired(true);
        }
    }
}
