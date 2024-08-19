using Domain.Abstracts;

namespace Domain.Chats
{
    public static class ChatErrors
    {
        public static Error AlreadyExist = new(
            "Chat.AlreadyExist",
            "The chat already exists.");
    }
}
