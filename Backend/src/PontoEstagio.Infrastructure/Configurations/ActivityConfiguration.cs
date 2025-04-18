using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PontoEstagio.Domain.Entities;

namespace PontoEstagio.Infrastructure.Configurations
{
    public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.ToTable("Atividades");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Description)
                   .IsRequired()
                   .HasMaxLength(1000);

            builder.Property(a => a.RecordedAt)
                   .IsRequired();

            // Relacionamento com User
            builder.HasOne(a => a.User)
                   .WithMany(u => u.Activities)
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento com Project
            builder.HasOne(a => a.Project)
                   .WithMany(p => p.Activities)
                   .HasForeignKey(a => a.ProjectId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(a => a.ProofFilePath)
                   .HasMaxLength(255);
        }
    }
}
