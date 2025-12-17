using MediatR;
using Mediaspot.Domain.Titles;
using Mediaspot.Application.Common;

namespace Mediaspot.Application.Titles.Queries.GetAllPaginated;

public sealed record GetAllTitlesPaginatedQuery(int Page = 1, int PageSize = 20) : IRequest<PaginatedResult<Title>>;
