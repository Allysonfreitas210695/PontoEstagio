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

        builder.OwnsOne(u => u.Address, address =>
        {
            address.Property(a => a.Street)
                .HasColumnName("Street")
                .IsRequired()
                .HasMaxLength(100);

            address.Property(a => a.Number)
                .HasColumnName("Number")
                .IsRequired()
                .HasMaxLength(20);

            address.Property(a => a.District)
                .HasColumnName("District")
                .IsRequired()
                .HasMaxLength(50);

            address.Property(a => a.City)
                .HasColumnName("City")
                .IsRequired()
                .HasMaxLength(50);

            address.Property(a => a.State)
                .HasColumnName("State")
                .IsRequired()
                .HasMaxLength(2);

            address.Property(a => a.ZipCode)
                .HasColumnName("ZipCode")
                .IsRequired()
                .HasMaxLength(8);

            address.Property(a => a.Complement)
                .HasColumnName("Complement")
                .HasMaxLength(100);
        });

        builder.Property(c => c.IsActive)
            .IsRequired();

        builder.HasMany(c => c.Projects)
               .WithOne(p => p.Company) 
               .HasForeignKey(p => p.CompanyId)  
               .OnDelete(DeleteBehavior.Restrict);
    }
}
