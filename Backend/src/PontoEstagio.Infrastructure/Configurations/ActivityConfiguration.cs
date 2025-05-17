using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PontoEstagio.Domain.Entities;

namespace PontoEstagio.Infrastructure.Configurations;

public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.ToTable("Activities");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Description)
               .IsRequired()
               .HasMaxLength(1000);

        builder.Property(a => a.RecordedAt)
               .IsRequired();

        builder.Property(a => a.Status)
               .IsRequired();

        builder.HasOne(a => a.User)
               .WithMany(u => u.Activities)
               .HasForeignKey(a => a.UserId)
               .OnDelete(DeleteBehavior.Restrict);

       builder.HasOne(a => a.Attendance)
              .WithMany(a => a.Activities)
              .HasForeignKey(a => a.AttendanceId)
              .OnDelete(DeleteBehavior.Cascade);

       builder.Property(a => a.ProofFilePath)
              .HasMaxLength(255);
    }
}

