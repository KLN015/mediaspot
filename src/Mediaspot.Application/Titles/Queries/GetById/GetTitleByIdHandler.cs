using MediatR;
using Mediaspot.Application.Exceptions;
using Mediaspot.Domain.Repositories;
using Mediaspot.Domain.Titles;

namespace Mediaspot.Application.Titles.Queries.GetById;

public sealed class GetTitleByIdHandler(
    ITitleRepository repo)
    : IRequestHandler<GetTitleByIdQuery, Title>
{
    public async Task<Title> Handle(GetTitleByIdQuery request, CancellationToken ct)
    {
        return await repo.GetByIdAsync(request.Id, ct) ?? throw new EntityNotFoundException(request.Id);
    }
}