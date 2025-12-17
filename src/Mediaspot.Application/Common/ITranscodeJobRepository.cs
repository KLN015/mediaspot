using Mediaspot.Domain.Transcoding;

namespace Mediaspot.Application.Common;

public interface ITranscodeJobRepository
{
    Task AddAsync(TranscodeJob job, CancellationToken ct);
    Task<TranscodeJob?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<TranscodeJob>> GetPendingAsync(int take, CancellationToken ct);
    Task<bool> HasActiveJobsAsync(Guid assetId, CancellationToken ct);
}
