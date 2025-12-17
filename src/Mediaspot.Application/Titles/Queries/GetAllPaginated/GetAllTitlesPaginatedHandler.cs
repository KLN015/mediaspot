using MediatR;
using Mediaspot.Application.Common;
using Mediaspot.Domain.Repositories;
using Mediaspot.Domain.Titles;

namespace Mediaspot.Application.Titles.Queries.GetAllPaginated;

public sealed class GetAllTitlesPaginatedHandler(ITitleRepository repo)
    : IRequestHandler<GetAllTitlesPaginatedQuery, PaginatedResult<Title>>
{
    public async Task<PaginatedResult<Title>> Handle(GetAllTitlesPaginatedQuery request, CancellationToken ct)
    {
        var page = request.Page < 1 ? 1 : request.Page;
        var pageSize = request.PageSize is < 1 or > 100 ? 20 : request.PageSize;

        var totalCount = await repo.CountAsync(ct);
        var items = await repo.GetPaginatedAsync(page, pageSize, ct);

        return new PaginatedResult<Title>(
            items,
            page,
            pageSize,
            totalCount
        );
    }
}
