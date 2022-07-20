using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;
using Zeta.CodebaseExpress.Application.Services.DateAndTime;
using Zeta.CodebaseExpress.Application.Services.Persistence;
using Zeta.CodebaseExpress.Domain.Entities;
using Zeta.CodebaseExpress.Domain.Interfaces;

namespace Zeta.CodebaseExpress.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IDateAndTimeService _dateTime = default!;

    public DbSet<ToDoGroup> ToDoGroups => Set<ToDoGroup>();
    public DbSet<ToDoItem> ToDoItems => Set<ToDoItem>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateAndTimeService dateTime)
        : base(options)
    {
        _dateTime = dateTime;
    }

    protected ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<ICreatable>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Created = _dateTime.Now;
                    break;
            }
        }

        foreach (var entry in ChangeTracker.Entries<IModifiable>())
        {
            switch (entry.State)
            {
                case EntityState.Modified:
                    entry.Entity.Modified = _dateTime.Now;
                    break;
            }
        }

        return await base.SaveChangesAsync(true, cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
