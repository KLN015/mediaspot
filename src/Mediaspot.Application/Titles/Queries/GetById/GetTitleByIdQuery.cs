using MediatR;
using Mediaspot.Domain.Titles;

namespace Mediaspot.Application.Titles.Queries.GetById;

public sealed record GetTitleByIdQuery(Guid Id): IRequest<Title>;