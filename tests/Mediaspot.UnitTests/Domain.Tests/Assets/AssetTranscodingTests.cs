using Mediaspot.Domain.Assets;
using Mediaspot.Domain.Assets.Events;
using Mediaspot.Domain.Assets.ValueObjects;
using Shouldly;

namespace Mediaspot.UnitTests.Domain.Tests.Assets;

public class AssetTranscodingTests
{
    [Fact]
    public void RequestTranscode_Should_Raise_TranscodeRequested_Event_For_Audio_And_Video()
    {
        var metadata = new Metadata("Title", "desc", "fr");

        Asset audio = new AudioAsset(
            externalId: Guid.NewGuid().ToString(),
            metadata: metadata,
            bitrate: 808,
            sampleRate: 3233,
            channels: 15
        );

        Asset video = new VideoAsset(
            externalId: Guid.NewGuid().ToString(),
            metadata: metadata,
            resolution: "1080px",
            frameRate: 60,
            codec: "RAW"
        );

        var audioFile = audio.RegisterMediaFile(
            new FilePath("/audio.mp3"),
            new Duration(TimeSpan.FromMinutes(5))
        );

        var videoFile = video.RegisterMediaFile(
            new FilePath("/video.mp4"),
            new Duration(TimeSpan.FromMinutes(120))
        );

        audio.RequestTranscode(audioFile, "MP3");
        video.RequestTranscode(videoFile, "1080p");

        audio.DomainEvents
            .OfType<TranscodeRequested>()
            .Count()
            .ShouldBe(1);

        video.DomainEvents
            .OfType<TranscodeRequested>()
            .Count()
            .ShouldBe(1);
    }
}
