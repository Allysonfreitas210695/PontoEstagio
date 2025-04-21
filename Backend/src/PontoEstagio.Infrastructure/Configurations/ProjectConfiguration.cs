using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PontoEstagio.Domain.Entities;

namespace PontoEstagio.Infrastructure.Configurations
{
    public class ProjetoConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projetos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(p => p.Description)
                   .HasMaxLength(500);

            builder.Property(p => p.Status)
               .IsRequired()
               .HasConversion<string>()
               .HasMaxLength(20);

            builder.Property(p => p.StartDate)
                   .IsRequired();

            builder.Property(p => p.EndDate)
                   .IsRequired(false);

            builder.Property(p => p.TotalHours)
                     .IsRequired();

            builder.HasOne(p => p.Creator)
                   .WithMany()
                   .HasForeignKey(p => p.CreatedBy)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.UserProjects)
                   .WithOne(up => up.Project)
                   .HasForeignKey(up => up.ProjectId)
                   .OnDelete(DeleteBehavior.Restrict);
 
            builder.HasMany(p => p.Activities)
                   .WithOne(a => a.Project)
                   .HasForeignKey(a => a.ProjectId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
