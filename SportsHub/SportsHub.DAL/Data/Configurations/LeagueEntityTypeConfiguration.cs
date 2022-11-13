using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsHub.DAL.Data.Configurations.Constants;
using SportsHub.Domain.Models;

namespace SportsHub.DAL.Data.Configurations
{
    public class LeagueEntityTypeConfiguration : IEntityTypeConfiguration<League>
    {
        public void Configure(EntityTypeBuilder<League> league)
        {
            league.Property(x => x.Name)
                .IsRequired(true)
                .HasMaxLength(ConfigurationConstants.LeagueNameMaxLength)
                .IsUnicode(true);

            league.Property(x => x.Description)
                .IsRequired(false)
                .HasMaxLength(ConfigurationConstants.LeagueDescriptionMaxLength)
                .IsUnicode(true);

            league.HasMany(x => x.SubLeagues)
                .WithOne(x => x.League)
                .HasForeignKey(x => x.LeagueId);

            league.HasMany(x => x.Teams)
                .WithOne(x => x.League)
                .HasForeignKey(x => x.LeagueId);

            league.HasOne(x => x.Sport)
                .WithMany(x => x.Leagues)
                .HasForeignKey(x => x.SportId);
        }
    }
}
