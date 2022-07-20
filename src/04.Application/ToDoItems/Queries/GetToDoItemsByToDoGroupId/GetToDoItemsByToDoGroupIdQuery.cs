using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zeta.CodebaseExpress.Application.Common.Extensions;
using Zeta.CodebaseExpress.Application.Common.Mappings;
using Zeta.CodebaseExpress.Application.Services.Persistence;
using Zeta.CodebaseExpress.Domain.Entities;
using Zeta.CodebaseExpress.Shared.Common.Enums;
using Zeta.CodebaseExpress.Shared.Common.Requests;
using Zeta.CodebaseExpress.Shared.Common.Responses;
using Zeta.CodebaseExpress.Shared.ToDoItems.Queries.GetToDoItemsByToDoGroupId;

namespace Zeta.CodebaseExpress.Application.ToDoItems.Queries.GetToDoItemsByToDoGroupId;

public class GetToDoItemsByToDoGroupIdQuery : PaginatedListRequest, IRequest<PaginatedListResponse<GetToDoItemsByToDoGroupIdToDoItem>>
{
    public Guid ToDoGroupId { get; set; }
}

public class GetToDoItemsByToDoGroupIdQueryValidator : AbstractValidator<GetToDoItemsByToDoGroupIdQuery>
{
    public GetToDoItemsByToDoGroupIdQueryValidator()
    {
        RuleFor(v => v.ToDoGroupId)
            .NotEmpty();

        Include(new PaginatedListRequestValidator());
    }
}

public class GetToDoItemsByToDoGroupIdToDoItemMapping : IMapFrom<ToDoItem, GetToDoItemsByToDoGroupIdToDoItem>
{
}

public class GetToDoItemsByToDoGroupIdQueryHandler : IRequestHandler<GetToDoItemsByToDoGroupIdQuery, PaginatedListResponse<GetToDoItemsByToDoGroupIdToDoItem>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetToDoItemsByToDoGroupIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedListResponse<GetToDoItemsByToDoGroupIdToDoItem>> Handle(GetToDoItemsByToDoGroupIdQuery request, CancellationToken cancellationToken)
    {
        var query = _context.ToDoItems
            .AsNoTracking()
            .Where(x => x.ToDoGroupId == request.ToDoGroupId);

        if (!string.IsNullOrEmpty(request.SearchText))
        {
            query = query.Where(x => x.Title.Contains(request.SearchText)
                || x.Description!.Contains(request.SearchText));
        }

        if (request.SortOrder is SortOrder.Asc)
        {
            query = request.SortField switch
            {
                nameof(GetToDoItemsByToDoGroupIdToDoItem.Title) => query.OrderBy(x => x.Title),
                nameof(GetToDoItemsByToDoGroupIdToDoItem.Description) => query.OrderBy(x => x.Description),
                nameof(GetToDoItemsByToDoGroupIdToDoItem.Created) => query.OrderBy(x => x.Created),
                nameof(GetToDoItemsByToDoGroupIdToDoItem.Modified) => query.OrderBy(x => x.Modified),
                nameof(GetToDoItemsByToDoGroupIdToDoItem.PriorityLevel) => query.OrderBy(x => x.PriorityLevel),
                nameof(GetToDoItemsByToDoGroupIdToDoItem.IsDone) => query.OrderBy(x => x.IsDone),
                _ => query.OrderBy(x => x.Created)
            };
        }
        else if (request.SortOrder is SortOrder.Desc)
        {
            query = request.SortField switch
            {
                nameof(GetToDoItemsByToDoGroupIdToDoItem.Title) => query.OrderByDescending(x => x.Title),
                nameof(GetToDoItemsByToDoGroupIdToDoItem.Description) => query.OrderByDescending(x => x.Description),
                nameof(GetToDoItemsByToDoGroupIdToDoItem.Created) => query.OrderByDescending(x => x.Created),
                nameof(GetToDoItemsByToDoGroupIdToDoItem.Modified) => query.OrderByDescending(x => x.Modified),
                nameof(GetToDoItemsByToDoGroupIdToDoItem.PriorityLevel) => query.OrderByDescending(x => x.PriorityLevel),
                nameof(GetToDoItemsByToDoGroupIdToDoItem.IsDone) => query.OrderByDescending(x => x.IsDone),
                _ => query.OrderBy(x => x.Created)
            };
        }
        else
        {
            query = query.OrderBy(x => x.Created);
        }

        var result = await query
            .ProjectTo<GetToDoItemsByToDoGroupIdToDoItem>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.Page, request.PageSize, cancellationToken);

        return result.ToPaginatedListResponse();
    }
}
