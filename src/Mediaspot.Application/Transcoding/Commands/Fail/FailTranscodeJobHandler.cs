using MediatR;
using Mediaspot.Domain.Transcoding;
using Mediaspot.Application.Exceptions;
using Mediaspot.Application.Common;

namespace Mediaspot.Application.Transcoding.Commands.Fail;

public sealed class FailTranscodeJobHandler(ITranscodeJobRepository repository, IUnitOfWork uow)
    : IRequestHandler<FailTranscodeJobCommand>
{
    public async Task Handle(FailTranscodeJobCommand request, CancellationToken ct)
    {
        var job = await repository.GetByIdAsync(request.JobId, ct) ?? throw new EntityNotFoundException(request.JobId);

        job.Fail(request.Reason);

        await uow.SaveChangesAsync(ct);
    }
}
