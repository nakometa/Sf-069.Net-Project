using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsHub.Domain.Models;

namespace SportsHub.DAL.Data.Configurations
{
    public class CommentLikeEntityTypeConfiguration : IEntityTypeConfiguration<CommentLike>
    {
        public void Configure(EntityTypeBuilder<CommentLike> commentLike)
        {
            commentLike.HasKey(x => new
            {
                x.UserId,
                x.CommentId
            });

            commentLike.Property(x => x.IsLike)
                .IsRequired();
        }
    }
}
