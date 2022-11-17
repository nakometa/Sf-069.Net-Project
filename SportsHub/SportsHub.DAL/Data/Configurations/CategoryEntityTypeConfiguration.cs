using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsHub.DAL.Data.Configurations.Constants;
using SportsHub.Domain.Models;

namespace SportsHub.DAL.Data.Configurations
{
    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> category)
        {
            category.Property(x => x.Name)
                .IsRequired(true)
                .HasMaxLength(ConfigurationConstants.CategoryConstants.CategoryNameMaxLength)
                .IsUnicode(true);

            category.Property(x => x.Description)
                .IsRequired(false)
                .HasMaxLength(ConfigurationConstants.CategoryConstants.CategoryDescriptionMaxLength)
                .IsUnicode(true);

            category.HasMany(x => x.Articles)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryId);
        }
    }
}
