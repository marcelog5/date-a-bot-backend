namespace Domain.BotChats
{
    public class BotChatDto
    {
        public BotChatDto(
            string message, 
            string role, 
            string goal, 
            string backstory)
        {
            Message = message;
            Role = role;
            Goal = goal;
            Backstory = backstory;
        }

        public string Message { get; private set; }
        public string Role { get; private set; }
        public string Goal { get; private set; }
        public string Backstory { get; private set; }
    }
}
