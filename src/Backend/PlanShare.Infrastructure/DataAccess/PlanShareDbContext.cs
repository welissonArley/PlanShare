using Microsoft.EntityFrameworkCore;
using PlanShare.Domain.Entities;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("WebApi.Tests")]
namespace PlanShare.Infrastructure.DataAccess;
internal sealed class PlanShareDbContext : DbContext
{
    public PlanShareDbContext(DbContextOptions options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<WorkItem> WorkItems { get; set; }
    public DbSet<UserConnection> UserConnections { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Assignee>().ToTable("Assignees");
    }
}