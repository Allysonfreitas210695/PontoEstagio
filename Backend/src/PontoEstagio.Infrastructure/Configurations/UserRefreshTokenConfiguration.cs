using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PontoEstagio.Domain.Entities;

namespace PontoEstagio.Infrastructure.Configurations;

 public class UserRefreshTokenConfiguration : IEntityTypeConfiguration<UserRefreshToken>
    {
        public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
        {
            builder.ToTable("UserRefreshTokens");

            builder.HasKey(urt => urt.Id);

            builder.Property(urt => urt.Token)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(urt => urt.ExpirationDate)
                   .IsRequired();

            builder.HasOne(urt => urt.User)
                   .WithMany()  
                   .HasForeignKey(urt => urt.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }