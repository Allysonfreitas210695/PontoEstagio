using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PontoEstagio.Domain.Entities;

namespace PontoEstagio.Infrastructure.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.OwnsOne(u => u.Email, email =>
            {
                email.Property(e => e.Endereco)
                     .HasColumnName("Email")
                     .IsRequired() 
                     .HasMaxLength(100);  

                 email.HasIndex(e => e.Endereco)
                     .IsUnique(); 
            });

            builder.Property(u => u.Type)
                   .IsRequired()
                   .HasConversion<string>()
                   .HasMaxLength(20);

            builder.Property(u => u.Password)
                   .HasColumnName("Password")
                   .IsRequired()
                   .HasMaxLength(100);
             
            builder.HasMany(u => u.UserProjects)
                   .WithOne(up => up.User)
                   .HasForeignKey(up => up.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Activities)
                   .WithOne(a => a.User)
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Attendances)
                   .WithOne(a => a.User)
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
