using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PontoEstagio.Domain.Entities;

namespace PontoEstagio.Infrastructure.Configurations
{
    public class FrequenciaConfiguration : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            builder.ToTable("Frequencias");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.Date)
                   .IsRequired();

            builder.Property(f => f.CheckIn)
                   .IsRequired();

            builder.Property(f => f.CheckOut)
                   .IsRequired();

            builder.Property(f => f.ProofImageBase64)
                   .IsRequired();

            builder.Property(u => u.Status)
                   .IsRequired()
                   .HasConversion<string>()
                   .HasMaxLength(20);

            builder.HasOne(f => f.User)
                   .WithMany(u => u.Attendances)
                   .HasForeignKey(f => f.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
