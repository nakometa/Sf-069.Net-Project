using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsHub.DAL.Data.Configurations.Constants;
using SportsHub.Domain.Models;

namespace SportsHub.DAL.Data.Configurations
{
    public class StateEntityTypeConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> state)
        {
            state.Property(x => x.Name)
                .IsRequired(true)
                .HasMaxLength(ConfigurationConstants.stateNameMaxLenth)
                .IsUnicode(true);

            state.HasMany(x => x.Articles)
                .WithOne(x => x.State)
                .HasForeignKey(x => x.StateId);
        }
    }
}
