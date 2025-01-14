using Microsoft.EntityFrameworkCore;
using PlanShare.Domain.Entities;

namespace PlanShare.Infrastructure.DataAccess;
public sealed class PlanShareDbContext : DbContext
{
    public PlanShareDbContext(DbContextOptions options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<WorkItem> WorkItems { get; set; }
    public DbSet<PersonAssociation> PersonAssociations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Assignee>().ToTable("Assignees");
    }
}