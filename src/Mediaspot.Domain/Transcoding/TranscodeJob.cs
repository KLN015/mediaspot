using Mediaspot.Domain.Common;
using Mediaspot.Domain.Transcoding.Events;

namespace Mediaspot.Domain.Transcoding;

public enum TranscodeStatus { Pending, Running, Succeeded, Failed }

public sealed class TranscodeJob : AggregateRoot
{
    public Guid AssetId { get; private set; }
    public Guid MediaFileId { get; private set; }
    public string Preset { get; private set; }
    public TranscodeStatus Status { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private TranscodeJob() { AssetId = Guid.Empty; MediaFileId = Guid.Empty; Preset = string.Empty; }

    public TranscodeJob(Guid assetId, Guid mediaFileId, string preset)
    {
        AssetId = assetId;
        MediaFileId = mediaFileId;
        Preset = preset;
        Status = TranscodeStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }

    public void Start()
    {
        if (Status != TranscodeStatus.Pending)
            throw new InvalidOperationException("Only pending jobs can be started.");

        Status = TranscodeStatus.Running;
        UpdatedAt = DateTime.UtcNow;

        Raise(new TranscodeJobStarted(Id));
    }

    public void Succeed()
    {
        if (Status != TranscodeStatus.Running)
            throw new InvalidOperationException("Only running jobs can succeed.");

        Status = TranscodeStatus.Succeeded;
        UpdatedAt = DateTime.UtcNow;

        Raise(new TranscodeJobSucceeded(Id));
    }

    public void Fail(string reason)
    {
        if (Status != TranscodeStatus.Running)
            throw new InvalidOperationException("Only running jobs can fail.");

        Status = TranscodeStatus.Failed;
        UpdatedAt = DateTime.UtcNow;

        Raise(new TranscodeJobFailed(Id, reason));
    }
}