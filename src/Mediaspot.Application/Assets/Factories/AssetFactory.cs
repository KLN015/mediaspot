using Mediaspot.Application.Assets;
using Mediaspot.Application.Assets.Commands.Create;
using Mediaspot.Domain.Assets;
using Mediaspot.Domain.Assets.ValueObjects;

namespace Mediaspot.Application.Assets.Factories;

public static class AssetFactory
{
    public static Asset Create(CreateAssetCommand cmd)
    {
        var metadata = new Metadata(
            cmd.Title,
            cmd.Description,
            cmd.Language
        );

        return cmd.Type switch
        {
            AssetType.Audio => new AudioAsset(
                cmd.ExternalId,
                metadata,
                bitrate: cmd.Bitrate ?? throw new ArgumentException("Bitrate is required for audio assets"),
                sampleRate: cmd.SampleRate ?? throw new ArgumentException("SampleRate is required for audio assets"),
                channels: cmd.Channels ?? throw new ArgumentException("Channels is required for audio assets")
            ),

            AssetType.Video => new VideoAsset(
                cmd.ExternalId,
                metadata,
                resolution: cmd.Resolution ?? throw new ArgumentException("Resolution is required for video assets"),
                frameRate: cmd.FrameRate ?? throw new ArgumentException("FrameRate is required for video assets"),
                codec: cmd.Codec ?? throw new ArgumentException("Codec is required for video assets")
            ),

            _ => throw new ArgumentOutOfRangeException("Asset type not valid")
        };
    }
}
