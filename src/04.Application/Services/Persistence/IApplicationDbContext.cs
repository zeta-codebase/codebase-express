using Microsoft.EntityFrameworkCore;
using Zeta.CodebaseExpress.Domain.Entities;

namespace Zeta.CodebaseExpress.Application.Services.Persistence;

public interface IApplicationDbContext
{
    DbSet<ToDoGroup> ToDoGroups { get; }
    DbSet<ToDoItem> ToDoItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
