using MediatR;
using Mediaspot.Domain.Transcoding;
using Mediaspot.Application.Exceptions;
using Mediaspot.Application.Common;

namespace Mediaspot.Application.Transcoding.Commands.Start;

public sealed class StartTranscodeJobHandler(ITranscodeJobRepository repository, IUnitOfWork uow)
    : IRequestHandler<StartTranscodeJobCommand>
{
    public async Task Handle(StartTranscodeJobCommand request, CancellationToken ct)
    {
        var job = await repository.GetByIdAsync(request.JobId, ct) ?? throw new EntityNotFoundException(request.JobId);

        job.Start();

        await uow.SaveChangesAsync(ct);
    }
}
