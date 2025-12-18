using Mediaspot.Application.Common;
using Mediaspot.Application.Titles.Commands.Create;
using Mediaspot.Domain.Repositories;
using Mediaspot.Domain.Enums;
using Moq;
using Shouldly;
using Mediaspot.Domain.Titles;

namespace Mediaspot.UnitTests.Application.Tests.Titles;

public class CreateTitleHandlerTests
{
    [Fact]
    public async Task Handle_Should_Create_Title_When_Name_Is_Unique()
    {
        var repo = new Mock<ITitleRepository>();
        var uow  = new Mock<IUnitOfWork>();

        repo.Setup(r => r.ExistsWithNameAsync(It.IsAny<string>(),It.IsAny<CancellationToken>())).ReturnsAsync(false);

        repo.Setup(r => r.AddAsync(It.IsAny<Title>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        uow.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new CreateTitleHandler(repo.Object, uow.Object);
        var cmd = new CreateTitleCommand(
            "Inception",
            TitleType.Movie,
            "Super Film",
            null
        );

        var id = await handler.Handle(cmd, CancellationToken.None);

        id.ShouldNotBe(Guid.Empty);

        repo.Verify(r => r.AddAsync(It.IsAny<Title>(), It.IsAny<CancellationToken>()), Times.Once);

        uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}