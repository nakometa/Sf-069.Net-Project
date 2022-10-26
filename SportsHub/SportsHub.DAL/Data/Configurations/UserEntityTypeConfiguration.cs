using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsHub.DAL.Data.Configurations.Constants;
using SportsHub.Domain.Models;

namespace SportsHub.DAL.Data.Configurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> user)
        {
            user.Property(x => x.Email)
                .IsRequired(true)
                .HasMaxLength(ConfigurationConstants.userEmailMaxLenth)
                .IsUnicode(true);

            user.Property(x => x.Username)
                .IsRequired(true)
                .HasMaxLength(ConfigurationConstants.userUsernameMaxLenth)
                .IsUnicode(true);

            user.HasIndex(x => x.Username)
                .IsUnique();

            user.Property(x => x.DisplayName)
                .IsRequired(true)
                .HasMaxLength(ConfigurationConstants.userDisplayNameMaxLenth)
                .IsUnicode(true);

            user.Property(x => x.FirstName)
                .IsRequired(true)
                .HasMaxLength(ConfigurationConstants.userFirstNamelMaxLenth)
                .IsUnicode(true);

            user.Property(x => x.LastName)
                .IsRequired(true)
                .HasMaxLength(ConfigurationConstants.userLastNamelMaxLenth)
                .IsUnicode(true);

            user.Property(x => x.Password)
                .IsRequired(true)
                .HasMaxLength(ConfigurationConstants.userPasswordlMaxLenth)
                .IsUnicode(true);

            user.Property(x => x.ProfilePicture)
                .IsRequired(false);

            user.HasMany(x => x.Articles)
                .WithMany(x => x.Authors);

            user.HasOne(x => x.Role)
                .WithMany(x => x.Users);
        }
    }
}
