using MediatR;
using Mediaspot.Application.Exceptions;
using Mediaspot.Application.Common;
using Mediaspot.Domain.Repositories;

namespace Mediaspot.Application.Titles.Commands.Update;

public sealed class UpdateTitleHandler(ITitleRepository repo, IUnitOfWork uow)
    : IRequestHandler<UpdateTitleCommand, Guid>
{
    public async Task<Guid> Handle(UpdateTitleCommand request, CancellationToken ct)
    {
        var title = await repo.GetByIdAsync(request.Id, ct) ?? throw new EntityNotFoundException(request.Id);

        if (await repo.ExistsWithNameAsync(request.Name, ct) && !string.Equals(title.Name, request.Name, StringComparison.Ordinal))
        {
            throw new DuplicateEntityParameterException(request.Name);
        }

        title.Update(
            request.Name,
            request.Type,
            request.Description,
            request.ReleaseDate
        );

        await uow.SaveChangesAsync(ct);

        return title.Id;
    }
}