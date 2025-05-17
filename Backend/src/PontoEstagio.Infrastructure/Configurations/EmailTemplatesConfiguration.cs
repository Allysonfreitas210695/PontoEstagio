using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PontoEstagio.Domain.Entities;

namespace PontoEstagio.Infrastructure.Configurations;

public class EmailTemplatesConfiguration : IEntityTypeConfiguration<EmailTemplates>
{
    public void Configure(EntityTypeBuilder<EmailTemplates> builder){
        builder.ToTable("EmailTemplates");

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Title)
               .HasMaxLength(80)
               .IsRequired();
        
        builder.Property(f => f.Body)
               .IsRequired();

        builder.Property(f => f.Subject)
                .HasMaxLength(80)
               .IsRequired();
    }
}