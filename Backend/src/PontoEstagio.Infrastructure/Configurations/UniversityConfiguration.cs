using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Domain.ValueObjects;

namespace PontoEstagio.Infrastructure.Configurations;

public class UniversityConfiguration : IEntityTypeConfiguration<University>
{
    public void Configure(EntityTypeBuilder<University> builder)
    {
        builder.ToTable("Universities");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(u => u.Acronym)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(u => u.CNPJ)
            .IsRequired()
            .HasMaxLength(18);

        builder.Property(u => u.Phone)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(u => u.IsActive)
            .IsRequired();

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
    }
}
