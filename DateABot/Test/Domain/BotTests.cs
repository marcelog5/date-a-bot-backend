using Domain.Bots;
using Domain.Shared;

namespace Test.Domain
{
    public class BotTests
    {
        [Fact]
        public void Create_WhenCalledWithValidParameters_CreatesBot()
        {
            // Arrange
            var validName = new Name("ChatBot");
            var validRole = "Companion";
            var validGoal = "To assist and engage in friendly conversations";
            var validBackstory = "A friendly bot created to keep you company.";
            var validAvatar = "avatar_url";

            // Act
            var bot = new Bot(validName, validRole, validGoal, validBackstory, validAvatar);

            // Assert
            Assert.NotNull(bot);
            Assert.Equal(validName, bot.Name);
            Assert.Equal(validRole, bot.Role);
            Assert.Equal(validGoal, bot.Goal);
            Assert.Equal(validBackstory, bot.Backstory);
            Assert.Equal(validAvatar, bot.Avatar);
        }

        [Fact]
        public void Create_WhenCalledWithNullRole_ThrowsException()
        {
            // Arrange
            var validName = new Name("ChatBot");
            string nullRole = null;
            var validGoal = "To assist and engage in friendly conversations";
            var validBackstory = "A friendly bot created to keep you company.";
            var validAvatar = "avatar_url";

            // Act & Assert
            Assert.Throws<Exception>(() => new Bot(validName, nullRole, validGoal, validBackstory, validAvatar));
        }

        [Fact]
        public void Create_WhenCalledWithNullGoal_ThrowsException()
        {
            // Arrange
            var validName = new Name("ChatBot");
            var validRole = "Companion";
            string nullGoal = null;
            var validBackstory = "A friendly bot created to keep you company.";
            var validAvatar = "avatar_url";

            // Act & Assert
            Assert.Throws<Exception>(() => new Bot(validName, validRole, nullGoal, validBackstory, validAvatar));
        }

        [Fact]
        public void Create_WhenCalledWithNullBackstory_ThrowsException()
        {
            // Arrange
            var validName = new Name("ChatBot");
            var validRole = "Companion";
            var validGoal = "To assist and engage in friendly conversations";
            string nullBackstory = null;
            var validAvatar = "avatar_url";

            // Act & Assert
            Assert.Throws<Exception>(() => new Bot(validName, validRole, validGoal, nullBackstory, validAvatar));
        }

        [Fact]
        public void Create_WhenCalledWithNullAvatar_ThrowsException()
        {
            // Arrange
            var validName = new Name("ChatBot");
            var validRole = "Companion";
            var validGoal = "To assist and engage in friendly conversations";
            var validBackstory = "A friendly bot created to keep you company.";
            string nullAvatar = null;

            // Act & Assert
            Assert.Throws<Exception>(() => new Bot(validName, validRole, validGoal, validBackstory, nullAvatar));
        }
    }
}
