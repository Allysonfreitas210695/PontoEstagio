using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PontoEstagio.Domain.Entities;

namespace PontoEstagio.Infrastructure.Configurations;
public class LegalRepresentativeConfiguration : IEntityTypeConfiguration<LegalRepresentative>
{
    public void Configure(EntityTypeBuilder<LegalRepresentative> builder)
    {
        builder.ToTable("LegalRepresentatives");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.FullName)
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(r => r.CPF)
               .IsRequired()
               .HasMaxLength(14);

        builder.Property(r => r.Position)
               .IsRequired()
               .HasMaxLength(100);

        builder.OwnsOne(u => u.Email, email =>
        {
            email.Property(e => e.Endereco)
                 .HasColumnName("Email")
                 .IsRequired()
                 .HasMaxLength(100);
            email.HasIndex(e => e.Endereco)
                .IsUnique();
        });

        builder.HasOne(r => r.Company)
               .WithMany(c => c.LegalRepresentatives)
               .HasForeignKey(r => r.CompanyId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
