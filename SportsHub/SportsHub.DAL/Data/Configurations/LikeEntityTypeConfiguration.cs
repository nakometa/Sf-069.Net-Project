using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsHub.Domain.Models;

namespace SportsHub.DAL.Data.Configurations
{
    public class LikeEntityTypeConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> like)
        {
            like.HasKey(x => new { x.UserId, x.ArticleId });

            like.Property(x => x.CreatedOn)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired(true);
        }
    }
}
