using Mediaspot.Domain.Assets.ValueObjects;
using Mediaspot.Domain.Assets.Events;

namespace Mediaspot.Domain.Assets;

public sealed class AudioAsset : Asset
{
    public int Bitrate { get; private set; }
    public int SampleRate { get; private set; }
    public int Channels { get; private set; }

    private AudioAsset() { }

    public AudioAsset(string externalId, Metadata metadata, int bitrate, int sampleRate, int channels) : base(externalId, metadata)
    {
        Bitrate = bitrate;
        SampleRate = sampleRate;
        Channels = channels;
    }

    public override void RequestTranscode(MediaFile mediaFile, string preset)
    {
        Raise(new TranscodeRequested(Id, mediaFile.Id.Value, preset));
    }
}
