using Domain.Chats;

namespace Test.Domain
{
    public class ChatTests
    {
        [Fact]
        public void Create_WhenCalledWithValidParameters_CreatesChat()
        {
            // Arrange
            var validUserId = Guid.NewGuid();
            var validBotId = Guid.NewGuid();

            // Act
            var chat = new Chat(validUserId, validBotId);

            // Assert
            Assert.NotNull(chat);
            Assert.Equal(validUserId, chat.UserId);
            Assert.Equal(validBotId, chat.BotId);
        }

        [Fact]
        public void Create_WhenCalledWithEmptyUserId_ThrowsException()
        {
            // Arrange
            var emptyUserId = Guid.Empty;
            var validBotId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<Exception>(() => new Chat(emptyUserId, validBotId));
        }

        [Fact]
        public void Create_WhenCalledWithEmptyBotId_ThrowsException()
        {
            // Arrange
            var validUserId = Guid.NewGuid();
            var emptyBotId = Guid.Empty;

            // Act & Assert
            Assert.Throws<Exception>(() => new Chat(validUserId, emptyBotId));
        }

        [Fact]
        public void Create_WhenCalledWithEmptyUserIdAndBotId_ThrowsException()
        {
            // Arrange
            var emptyUserId = Guid.Empty;
            var emptyBotId = Guid.Empty;

            // Act & Assert
            Assert.Throws<Exception>(() => new Chat(emptyUserId, emptyBotId));
        }
    }
}
