using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zeta.CodebaseExpress.Application.Services.Persistence;
using Zeta.CodebaseExpress.Domain.Entities;
using Zeta.CodebaseExpress.Infrastructure.Persistence.Common.Constants;
using Zeta.CodebaseExpress.Shared.ToDoItems.Constants;

namespace Zeta.CodebaseExpress.Infrastructure.Persistence.SqlServer.Configuration;

public class ToDoItemConfiguration : IEntityTypeConfiguration<ToDoItem>
{
    public void Configure(EntityTypeBuilder<ToDoItem> builder)
    {
        builder.ToTable(nameof(IApplicationDbContext.ToDoItems), nameof(CodebaseExpress));

        builder.Property(e => e.Title).HasColumnType(CommonColumnTypes.Nvarchar(MaximumLengthFor.Title));
        builder.Property(e => e.Description).HasColumnType(CommonColumnTypes.Nvarchar(MaximumLengthFor.Description));

        builder.HasOne(e => e.ToDoGroup).WithMany(e => e.ToDoItems).HasForeignKey(e => e.ToDoGroupId).OnDelete(DeleteBehavior.Restrict);
    }
}
