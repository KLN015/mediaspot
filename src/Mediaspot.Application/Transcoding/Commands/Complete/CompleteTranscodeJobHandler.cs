using MediatR;
using Mediaspot.Domain.Transcoding;
using Mediaspot.Application.Exceptions;
using Mediaspot.Application.Common;

namespace Mediaspot.Application.Transcoding.Commands.Complete;

public sealed class CompleteTranscodeJobHandler(ITranscodeJobRepository repository, IUnitOfWork uow)
    : IRequestHandler<CompleteTranscodeJobCommand>
{
    public async Task Handle(CompleteTranscodeJobCommand request, CancellationToken ct)
    {
        var job = await repository.GetByIdAsync(request.JobId, ct) ?? throw new EntityNotFoundException(request.JobId);

        job.Succeed();

        await uow.SaveChangesAsync(ct);
    }
}
