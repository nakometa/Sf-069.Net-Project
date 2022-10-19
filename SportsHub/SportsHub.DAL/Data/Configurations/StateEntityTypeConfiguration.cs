using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsHub.Domain.Models;

namespace SportsHub.DAL.Data.Configurations
{
    public class StateEntityTypeConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> state)
        {
            state.Property(x => x.Name)
                .IsRequired(true)
                .HasMaxLength(30)
                .IsUnicode(false);
        }
    }
}
