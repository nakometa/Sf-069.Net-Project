using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsHub.DAL.Data.Configurations.Constants;
using SportsHub.Domain.Models;

namespace SportsHub.DAL.Data.Configurations
{
    public class TeamEntityTypeConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> team)
        {
            team.Property(x => x.Name)
                .IsRequired(true)
                .HasMaxLength(ConfigurationConstants.TeamNameMaxLength)
                .IsUnicode(true);

            team.Property(x => x.Description)
                .IsRequired(false)
                .HasMaxLength(ConfigurationConstants.TeamDescriptionMaxLength)
                .IsUnicode(true);

            team.HasOne(x => x.League)
                .WithMany(x => x.Teams)
                .HasForeignKey(x => x.LeagueId);
        }
    }
}
