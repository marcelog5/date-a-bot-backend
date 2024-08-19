using Domain.Shared;
using Domain.Users;

namespace Test.Domain
{
    public class UserTests
    {
        [Fact]
        public void Create_WhenCalledWithValidParameters_CreatesUser()
        {
            // Arrange
            var validName = new Name("John Doe");
            var validEmail = new Email("john.doe@example.com");
            var validPassword = "P@ssw0rd";

            // Act
            var user = new User(validName, validEmail, validPassword);

            // Assert
            Assert.NotNull(user);
            Assert.Equal(validName, user.Name);
            Assert.Equal(validEmail, user.Email);
            Assert.Equal(validPassword, user.Password);
        }

        [Fact]
        public void Create_WhenCalledWithNullPassword_ThrowsException()
        {
            // Arrange
            var validName = new Name("John Doe");
            var validEmail = new Email("john.doe@example.com");
            string nullPassword = null;

            // Act & Assert
            Assert.Throws<Exception>(() => new User(validName, validEmail, nullPassword));
        }

        [Fact]
        public void Create_WhenCalledWithValidParameters_CreatesUserWithExpectedProperties()
        {
            // Arrange
            var validName = new Name("Jane Doe");
            var validEmail = new Email("jane.doe@example.com");
            var validPassword = "Str0ngP@ssw0rd";

            // Act
            var user = new User(validName, validEmail, validPassword);

            // Assert
            Assert.Equal(validName, user.Name);
            Assert.Equal(validEmail, user.Email);
            Assert.Equal(validPassword, user.Password);
        }
    }
}
