using Domain.Abstracts;
using Domain.Shared;

namespace Domain.Bots
{
    public sealed class Bot : Entity
    {
        public Bot(
            Name name,
            string role,
            string goal,
            string backstory,
            string avatar)
            : base()
        {
            if (role == null)
            {
                throw new Exception("Role cannot be null.");
            }

            if (goal == null)
            {
                throw new Exception("Goal cannot be null.");
            }

            if (backstory == null)
            {
                throw new Exception("Backstory cannot be null.");
            }

            if (avatar == null)
            {
                throw new Exception("Avatar cannot be null.");
            }

            Name = name;
            Role = role;
            Goal = goal;
            Backstory = backstory;
            Avatar = avatar;
        }

        private Bot() 
        { 
        }
        
        public Name Name { get; private set; }
        public string Role { get; private set; }
        public string Goal { get; private set; }
        public string Backstory { get; private set; }
        public string Avatar { get; private set; }
    }
}
