using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PontoEstagio.Domain.Entities;

namespace PontoEstagio.Infrastructure.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.WorkloadHours)
            .IsRequired();

        builder.Property(c => c.UniversityId)
            .IsRequired();

        builder.HasOne(c => c.University)
            .WithMany(u => u.Courses)
            .HasForeignKey(c => c.UniversityId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}