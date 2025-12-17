using Mediaspot.Domain.Assets.Events;
using Mediaspot.Domain.Assets.ValueObjects;
using Mediaspot.Domain.Common;

namespace Mediaspot.Domain.Assets;

public abstract class Asset : AggregateRoot
{
    protected readonly List<MediaFile> _mediaFiles = [];

    public string ExternalId { get; protected set; }
    public Metadata Metadata { get; protected set; }
    public bool Archived { get; protected set; }

    public IReadOnlyCollection<MediaFile> MediaFiles => _mediaFiles.AsReadOnly();

    protected Asset() { ExternalId = string.Empty; Metadata = new("", null, null); }

    protected Asset(string externalId, Metadata metadata)
    {
        ExternalId = externalId;
        Metadata = metadata;
        Raise(new AssetCreated(Id));
    }

    public MediaFile RegisterMediaFile(FilePath path, Duration duration)
    {
        var mf = new MediaFile(MediaFileId.New(), path, duration);
        _mediaFiles.Add(mf);
        Raise(new MediaFileRegistered(Id, mf.Id.Value));
        return mf;
    }

    public void UpdateMetadata(Metadata metadata)
    {
        if (string.IsNullOrWhiteSpace(metadata.Title))
            throw new ArgumentException("Title is required");

        Metadata = metadata;
        Raise(new MetadataUpdated(Id));
    }

    public void Archive(Func<Guid, bool> hasActiveJobs)
    {
        if (Archived) return;
        if (hasActiveJobs(Id))
            throw new InvalidOperationException("Cannot archive while active transcode jobs exist.");

        Archived = true;
        Raise(new AssetArchived(Id));
    }

    public abstract void RequestTranscode(MediaFile mediaFile, string preset);
}
