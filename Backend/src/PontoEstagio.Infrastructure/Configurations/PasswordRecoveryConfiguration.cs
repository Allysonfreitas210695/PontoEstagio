using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PontoEstagio.Domain.Entities;

namespace PontoEstagio.Infrastructure.Configurations;
public class PasswordRecoveryConfiguration : IEntityTypeConfiguration<PasswordRecovery>
{
    public void Configure(EntityTypeBuilder<PasswordRecovery> builder)
    {
        builder.ToTable("PasswordRecoveries");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
               .IsRequired();

        builder.Property(p => p.UserId)
                   .IsRequired();

        builder.Property(p => p.Code)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(p => p.CreatedAt)
               .IsRequired();

        builder.Property(p => p.Used)
               .IsRequired();

        builder.HasOne(p => p.User)
               .WithMany()  
               .HasForeignKey(p => p.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }

}
