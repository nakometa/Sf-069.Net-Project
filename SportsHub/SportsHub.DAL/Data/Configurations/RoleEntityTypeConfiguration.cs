using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsHub.DAL.Data.Configurations.Constants;
using SportsHub.Domain.Models;
using SportsHub.Domain.Models.Constants;

namespace SportsHub.DAL.Data.Configurations
{
    public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> role)
        {
            role.Property(x => x.Name)
                .IsRequired(true)
                .HasMaxLength(RoleConstants.NameLength)
                .IsUnicode(false);
        }
    }
}
