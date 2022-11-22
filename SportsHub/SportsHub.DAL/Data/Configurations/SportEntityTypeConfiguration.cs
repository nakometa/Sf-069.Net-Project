using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsHub.DAL.Data.Configurations.Constants;
using SportsHub.Domain.Models;

namespace SportsHub.DAL.Data.Configurations
{
    public class SportEntityTypeConfiguration : IEntityTypeConfiguration<Sport>
    {
        public void Configure(EntityTypeBuilder<Sport> sport)
        {
            sport.Property(x => x.Name)
                .IsRequired(true)
                .HasMaxLength(ConfigurationConstants.SportConstants.SportNameMaxLength)
                .IsUnicode(true);

            sport.HasIndex(x => x.Name)
                .IsUnique();

            sport.Property(x => x.Description)
                .IsRequired(false)
                .HasMaxLength(ConfigurationConstants.SportConstants.SportDescriptionMaxLength)
                .IsUnicode(true);

            sport.HasMany(x => x.Leagues)
                .WithOne(x => x.Sport)
                .HasForeignKey(x => x.SportId);
        }
    }
}
