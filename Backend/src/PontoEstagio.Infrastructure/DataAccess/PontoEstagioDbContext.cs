using Microsoft.EntityFrameworkCore;
using PontoEstagio.Domain.Entities;

namespace PontoEstagio.Infrastructure.Context;

public class PontoEstagioDbContext : DbContext
{
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<Activity> Activitys { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<University> Universities { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<UserProject> UserProjects { get; set; }
    public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
    public DbSet<EmailTemplates> EmailTemplates { get; set; }
    public DbSet<PasswordRecovery> PasswordRecoveries { get; set; }
    public DbSet<LegalRepresentative> LegalRepresentatives { get; set; }
    public DbSet<VerificationCodeUniversity> VerificationCodeUniversities { get; set; }

    public PontoEstagioDbContext(DbContextOptions<PontoEstagioDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PontoEstagioDbContext).Assembly);
    }
}
