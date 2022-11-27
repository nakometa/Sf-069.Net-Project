using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsHub.Domain.Models;

namespace SportsHub.DAL.Data.Configurations
{
    public class DislikeEntityTypeConfiguration : IEntityTypeConfiguration<Dislike>
    {
        public void Configure(EntityTypeBuilder<Dislike> dislike)
        {
            dislike.HasKey(x => new { x.UserId, x.ArticleId });

            dislike.Property(x => x.CreatedOn)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired(true);
        }
    }
}
