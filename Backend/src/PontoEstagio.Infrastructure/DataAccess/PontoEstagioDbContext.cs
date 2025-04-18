using Microsoft.EntityFrameworkCore;
using PontoEstagio.Domain.Entities;

namespace PontoEstagio.Infrastructure.Context;

public class PontoEstagioDbContext : DbContext
{
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<Activity> Activitys { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserProject> UserProjects { get; set; }
    public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

    public PontoEstagioDbContext(DbContextOptions<PontoEstagioDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PontoEstagioDbContext).Assembly);
    }
}
