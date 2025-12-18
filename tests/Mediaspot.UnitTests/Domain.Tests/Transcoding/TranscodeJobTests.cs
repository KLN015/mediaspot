using Mediaspot.Domain.Transcoding;
using Mediaspot.Domain.Transcoding.Events;
using Shouldly;

namespace Mediaspot.UnitTests.Domain.Tests.Transcoding;

public class TranscodeJobTests
{
    [Fact]
    public void Start_Should_Set_Status_To_Running_And_Raise_Event()
    {
        var job = new TranscodeJob(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "1080px"
        );

        job.Start();

        job.Status.ShouldBe(TranscodeStatus.Running);
        job.UpdatedAt.ShouldNotBeNull();

        job.DomainEvents
            .OfType<TranscodeJobStarted>()
            .Count()
            .ShouldBe(1);
    }
}