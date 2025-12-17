using Mediaspot.Domain.Assets.ValueObjects;
using Mediaspot.Domain.Assets.Events;

namespace Mediaspot.Domain.Assets;

public sealed class VideoAsset : Asset
{
    public string Resolution { get; private set; }
    public int FrameRate { get; private set; }
    public string Codec { get; private set; }

    private VideoAsset()
    {
        Resolution = string.Empty;
        Codec = string.Empty;
    }

    public VideoAsset(string externalId, Metadata metadata, string resolution, int frameRate, string codec) : base(externalId, metadata)
    {
        Resolution = resolution;
        FrameRate = frameRate;
        Codec = codec;
    }

    public override void RequestTranscode(MediaFile mediaFile, string preset)
    {
        Raise(new TranscodeRequested(Id, mediaFile.Id.Value, preset));
    }
}
