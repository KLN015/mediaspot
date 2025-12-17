using Mediaspot.Application.Common;
using Mediaspot.Domain.Transcoding;
using Microsoft.EntityFrameworkCore;

namespace Mediaspot.Infrastructure.Persistence;

public sealed class TranscodeJobRepository(MediaspotDbContext db) : ITranscodeJobRepository
{
    public async Task AddAsync(TranscodeJob job, CancellationToken ct) => await db.TranscodeJobs.AddAsync(job, ct);

    public async Task<TranscodeJob?> GetByIdAsync(Guid id, CancellationToken ct)
        => await db.TranscodeJobs.FirstOrDefaultAsync(j => j.Id == id, ct);
    
    public async Task<IReadOnlyList<TranscodeJob>> GetPendingAsync(int take, CancellationToken ct)
        => await db.TranscodeJobs
            .Where(j => j.Status == TranscodeStatus.Pending)
            .OrderBy(j => j.CreatedAt)
            .Take(take)
            .ToListAsync(ct);

    public Task<bool> HasActiveJobsAsync(Guid assetId, CancellationToken ct)
        => db.TranscodeJobs.AnyAsync(j => j.AssetId == assetId && (j.Status == TranscodeStatus.Pending || j.Status == TranscodeStatus.Running), ct);
}
