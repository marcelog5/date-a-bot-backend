using Domain.Abstracts;

namespace Domain.Users
{
    public static class UserErrors
    {
        public static Error NotFound = new(
            "User.NotFound",
            "The user with the specified identifier could not be found.");

        public static Error AlreadyExists = new(
            "User.AlreadyExists",
            "The user with the specified identifier already exists.");
    }
}
