using Application.UseCases.BotUseCases;
using Domain.Abstracts;
using Domain.Bots;
using Domain.Shared;
using Moq;

namespace Test.Application
{
    public class CreateBotUseCaseTests
    {
        [Fact]
        public async Task Execute_WhenCalledWithValidInput_CreatesBot()
        {
            // Arrange
            var createBotInput = new CreateBotInput(
                new Name("BotName"),
                "Role",
                "Goal",
                "Backstory",
                "Avatar");

            var botRepoMock = new Mock<IBotRepository>();
            botRepoMock.Setup(x => x.Add(It.IsAny<Bot>(), new CancellationToken())).Verifiable();

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var createBotUseCase = new CreateBotUseCase(
                botRepoMock.Object,
                unitOfWorkMock.Object);

            // Act
            var result = await createBotUseCase.Execute(createBotInput, new CancellationToken());

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(Guid.Empty, result.Value.Id);
            botRepoMock.Verify(x => x.Add(It.IsAny<Bot>(), new CancellationToken()), Times.Once);
            unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
