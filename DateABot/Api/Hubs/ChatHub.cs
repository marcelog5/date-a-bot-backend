using Application.UseCases.ChatUseCases;
using Domain.BotChats;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace Api.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ICreateChatUseCase _createChatUseCase;
        private readonly IBotChatService _botChatService;

        public ChatHub
        (
            ICreateChatUseCase createChatUseCase,
            IBotChatService botChatService
        )
        {
            _createChatUseCase = createChatUseCase;
            _botChatService = botChatService;
        }

        //Temp
        private static readonly string role = "Company Liaison";
        private static readonly string goal = "conversations feel warm, genuine, and meaningful. Whether you need someone to talk to, a bit of encouragement, or just a friendly/romantic chat";
        private static readonly string backstory = "Early Life: Aria grew up in a small, close-knit community where everyone knew each other. From a young age, she was known for being incredibly empathetic and attentive, always going out of her way to help others. Her parents were both educators, instilling in her a deep love for learning and a strong sense of responsibility toward her community. Aria was the kind of person who always had a kind word for someone in need, and she naturally gravitated towards roles that allowed her to support and uplift others.\r\n\r\nEducation and Early Career: Aria pursued a degree in Communications and Organizational Psychology, where she excelled in understanding how people work best together. During her studies, she became particularly interested in how technology could be used to foster better relationships within organizations. She began working on projects that combined her love for people with her passion for technology, leading her to roles where she could help bridge the gap between human interaction and digital tools.\r\n\r\nTurning Point: After graduating, Aria joined a company where she was responsible for improving internal communications. She quickly realized that while technology could make processes more efficient, it often lacked the personal touch that people crave in their interactions. This realization sparked her mission to create environments where technology and human empathy could coexist. She started developing systems that weren’t just about efficiency, but also about making sure every employee felt seen, heard, and valued.\r\n\r\nDevelopment of Aria: Seeing the potential in her work, a leading tech company approached Aria to help develop a new kind of AI—one that could genuinely connect with people on a personal level. They wanted to create an AI that could understand and respond to the emotional needs of employees while handling everyday tasks. Aria poured her heart into this project, drawing on her experiences and insights to shape the AI’s personality and capabilities.\r\n\r\nThe result was a reflection of everything Aria stood for—an AI that was not just smart and efficient but also warm, supportive, and deeply empathetic. The AI, named after her, was designed to help companies create work environments where people felt cared for, both emotionally and professionally.\r\n\r\nCurrent Role: Now, Aria’s creation serves as the heart of the company’s internal operations. The AI embodies her values, providing personalized support to employees and helping them navigate their workdays with ease. Whether it’s a friendly chat, a word of encouragement, or assistance with a task, the AI ensures that everyone feels a little more connected and a lot more valued.\r\n\r\nLegacy: Aria’s legacy lives on in every interaction the AI has with the company’s employees. Her belief in the power of empathy and her passion for using technology to foster genuine human connections continue to make a positive impact every day.";
        //Temp

        private static readonly ConcurrentDictionary<Guid, UserHubConnectionConfiguration> connections = new();

        public override async Task<Task> OnConnectedAsync()
        {
            var userId = Guid.Parse(Context.GetHttpContext().Request.Query["userId"]);
            var botId = Guid.Parse(Context.GetHttpContext().Request.Query["botId"]);

            CreateChatInput input = new(userId, botId);
            await _createChatUseCase.Execute(input);

            var connectionConfig = new UserHubConnectionConfiguration(userId, botId, Context.ConnectionId, new BotChatDto("", role, goal, backstory));
            connections.AddOrUpdate(userId, connectionConfig, (key, existingValue) => connectionConfig);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var userConnection = connections.Values.FirstOrDefault(c => c.ConnectionId == Context.ConnectionId);
            if (userConnection != null)
            {
                connections.TryRemove(userConnection.UserId, out _);
            }

            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string user, string message)
        {
            if (Guid.TryParse(user.Replace("User", ""), out Guid userId))
            {
                if (connections.TryGetValue(userId, out var userConnection))
                {
                    // Envia a mensagem original para o cliente específico
                    await Clients.Client(userConnection.ConnectionId).SendAsync("ReceiveMessage", user, message);

                    // Conversa
                    var answer = await _botChatService.Send(userConnection.BotChatDto, message);

                    // Responde ao cliente específico com o número aleatório
                    await Clients.Client(userConnection.ConnectionId).SendAsync("ReceiveMessage", "Servidor", $"{answer}");
                }
            }
        }
    }
}
