using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PontoEstagio.Domain.Entities;

namespace PontoEstagio.Infrastructure.Configurations;
public class UserProjectConfiguration : IEntityTypeConfiguration<UserProject>
{
        public void Configure(EntityTypeBuilder<UserProject> builder)
        {
            builder.ToTable("UserProjects");

            builder.HasKey(up => up.Id);

            builder.Property(up => up.AssignedAt)
                   .IsRequired();

            builder.Property(up => up.Role)
                   .IsRequired()
                   .HasConversion<string>() 
                   .HasMaxLength(20);  

            builder.HasOne(up => up.User)
                   .WithMany(u => u.UserProjects)
                   .HasForeignKey(up => up.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(up => up.Project)
                   .WithMany(p => p.UserProjects)
                   .HasForeignKey(up => up.ProjectId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
}
