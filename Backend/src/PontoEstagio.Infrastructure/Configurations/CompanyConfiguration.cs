using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PontoEstagio.Domain.Entities;

namespace PontoEstagio.Infrastructure.Configurations;
public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.CNPJ)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(c => c.Phone)
            .IsRequired(false)
            .HasMaxLength(20);

        builder.OwnsOne(u => u.Email, email =>
        {
            email.Property(e => e.Endereco)
                 .HasColumnName("Email")
                 .IsRequired() 
                 .HasMaxLength(100);  
             email.HasIndex(e => e.Endereco)
                 .IsUnique(); 
        });

        builder.Property(c => c.IsActive)
            .IsRequired();

        builder.HasMany(c => c.Projects)
               .WithOne(p => p.Company) 
               .HasForeignKey(p => p.CompanyId)  
               .OnDelete(DeleteBehavior.Restrict);
    }
}
