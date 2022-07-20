using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zeta.CodebaseExpress.Application.Services.Persistence;
using Zeta.CodebaseExpress.Domain.Entities;
using Zeta.CodebaseExpress.Infrastructure.Persistence.Common.Constants;
using Zeta.CodebaseExpress.Shared.ToDoGroups.Constants;

namespace Zeta.CodebaseExpress.Infrastructure.Persistence.SqlServer.Configuration;

public class ToDoGroupConfiguration : IEntityTypeConfiguration<ToDoGroup>
{
    public void Configure(EntityTypeBuilder<ToDoGroup> builder)
    {
        builder.ToTable(nameof(IApplicationDbContext.ToDoGroups), nameof(CodebaseExpress));

        builder.Property(e => e.Name).HasColumnType(CommonColumnTypes.Nvarchar(MaximumLengthFor.Name));
    }
}
