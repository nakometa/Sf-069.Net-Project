using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsHub.DAL.Data.Configurations.Constants;
using SportsHub.Domain.Models;

namespace SportsHub.DAL.Data.Configurations
{
    public class SubLeagueEntityTypeConfiguration : IEntityTypeConfiguration<SubLeague>
    {
        public void Configure(EntityTypeBuilder<SubLeague> subLeague)
        {
            subLeague.Property(x => x.Name)
                .IsRequired(true)
                .HasMaxLength(ConfigurationConstants.SubLeagueConstants.SubLeagueNameMaxLength)
                .IsUnicode(true);

            subLeague.HasIndex(x => x.Name)
                .IsUnique();

            subLeague.Property(x => x.Description)
                .IsRequired(false)
                .HasMaxLength(ConfigurationConstants.SubLeagueConstants.SubLeagueDescriptionMaxLength)
                .IsUnicode(true);

            subLeague.HasOne(x => x.League)
                .WithMany(x => x.SubLeagues)
                .HasForeignKey(x => x.LeagueId);
        }
    }
}
