using Domain.Abstracts;
using Domain.Shared;

namespace Domain.Users
{
    public sealed class User : Entity
    {
        public User(
            Name name, 
            Email email, 
            string password)
            : base()
        {
            if (password == null)
            {
                throw new Exception("Password cannot be null");
            }

            Name = name;
            Email = email;
            Password = password;
        }

        private User() 
        { 
        }

        public Name Name { get; private set; }
        public Email Email { get; private set; }
        public string Password { get; private set; }
    }
}
