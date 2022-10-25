using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsHub.Domain.Models;
using SportsHub.Domain.Models.Constants;

namespace SportsHub.DAL.Data.Configurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> user)
        {
            user.Property(x => x.Email)
                .IsRequired(true)
                .HasMaxLength(UserConstants.EmailLength)
                .IsUnicode(false);

            user.Property(x => x.Username)
                .IsRequired(true)
                .HasMaxLength(UserConstants.UsernameLength)
                .IsUnicode(false);

            user.HasIndex(x => x.Username)
                .IsUnique();

            user.Property(x => x.DisplayName)
                .IsRequired(true)
                .HasMaxLength(UserConstants.DisplayNameLength)
                .IsUnicode(false);

            user.Property(x => x.FirstName)
                .IsRequired(true)
                .HasMaxLength(UserConstants.FirstNameLength)
                .IsUnicode(false);

            user.Property(x => x.LastName)
                .IsRequired(true)
                .HasMaxLength(UserConstants.LastNameLength)
                .IsUnicode(false);

            user.Property(x => x.Password)
                .IsRequired(true)
                .HasMaxLength(UserConstants.PasswordLength)
                .IsUnicode(false);

            user.Property(x => x.ProfilePicture)
                .IsRequired(false);
        }
    }
}
