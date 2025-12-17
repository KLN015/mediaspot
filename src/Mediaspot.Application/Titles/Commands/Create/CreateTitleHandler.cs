using MediatR;
using Mediaspot.Application.Exceptions;
using Mediaspot.Application.Common;
using Mediaspot.Domain.Titles;
using Mediaspot.Domain.Repositories;

namespace Mediaspot.Application.Titles.Commands.Create;

public sealed class CreateTitleHandler(ITitleRepository repo, IUnitOfWork uow)
    : IRequestHandler<CreateTitleCommand, Guid>
{
    public async Task<Guid> Handle(CreateTitleCommand request, CancellationToken ct)
    {
        if (await repo.ExistsWithNameAsync(request.Name, ct))
            throw new DuplicateEntityParameterException(request.Name);

        var title = new Title(
            request.Name,
            request.Type,
            request.Description,
            request.ReleaseDate
        );

        await repo.AddAsync(title, ct);
        await uow.SaveChangesAsync(ct);

        return title.Id;
    }
}