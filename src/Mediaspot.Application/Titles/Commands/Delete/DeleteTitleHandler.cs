using MediatR;
using Mediaspot.Application.Exceptions;
using Mediaspot.Application.Common;
using Mediaspot.Domain.Repositories;

namespace Mediaspot.Application.Titles.Commands.Delete;

public sealed class DeleteTitleHandler(ITitleRepository repo, IUnitOfWork uow)
    : IRequestHandler<DeleteTitleCommand, Guid>
{
    public async Task<Guid> Handle(DeleteTitleCommand request, CancellationToken ct)
    {
        var title = await repo.GetByIdAsync(request.Id, ct) ?? throw new EntityNotFoundException(request.Id);

        await repo.DeleteAsync(title, ct);
        await uow.SaveChangesAsync(ct);

        return title.Id;
    }
}