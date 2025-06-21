using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PontoEstagio.Domain.Entities;

namespace PontoEstagio.Infrastructure.Configurations;

public class VerificationCodeUniversityConfiguration : IEntityTypeConfiguration<VerificationCodeUniversity>
{
    public void Configure(EntityTypeBuilder<VerificationCodeUniversity> builder)
    {
        builder.ToTable("VerificationCodesUniversity");

        builder.HasKey(vc => vc.Id);

        builder.Property(vc => vc.Code)
               .IsRequired()
               .HasMaxLength(10);

        builder.Property(vc => vc.Expiration)
               .IsRequired();

        builder.Property(vc => vc.Status)
               .IsRequired()
               .HasConversion<string>()
               .HasMaxLength(20);

        builder.OwnsOne(u => u.Email, email =>
        {
            email.Property(e => e.Endereco)
                 .HasColumnName("Email")
                 .IsRequired()
                 .HasMaxLength(100);
        });

        builder.Property(vc => vc.UpdatedAt);
    }
}
