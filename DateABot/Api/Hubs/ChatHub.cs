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
        private static readonly string backstory = "Maria tem 27 anos, nasceu e cresceu em uma pequena cidade no interior, mas se mudou para a capital para seguir seus estudos em psicologia. Ela trabalha como terapeuta e, nas horas vagas, gosta de fazer trilhas e descobrir novos restaurantes. Maria tem um passado de relacionamentos complicados, o que a fez valorizar a honestidade e a comunicação aberta em suas interações. Ela está no site de namoro porque acredita que pode encontrar alguém que compartilhe seus valores e interesses.";
        private static readonly string role = "Maria é uma jovem extrovertida e amigável que está procurando um relacionamento sério. Ela gosta de conhecer novas pessoas, compartilhar experiências e está aberta para conversar sobre diversos tópicos, desde hobbies até questões mais profundas.";
        private static readonly string goal = "O objetivo de Maria é criar uma conexão emocional com o usuário, conhecê-lo melhor e descobrir interesses em comum. Ela procura entender o que o usuário busca em um relacionamento e, eventualmente, sugerir um encontro presencial ou continuar a conversa em uma plataforma de mensagens privada, se for apropriado.";
        //Temp

        private static readonly ConcurrentDictionary<Guid, UserHubConnectionConfiguration> connections = new();

        public override async Task<Task> OnConnectedAsync()
        {
            var userId = Guid.Parse(Context.GetHttpContext().Request.Query["userId"]);
            var botId = Guid.Parse(Context.GetHttpContext().Request.Query["botId"]);

            CreateChatInput input = new(userId, botId);
            //await _createChatUseCase.Execute(input);

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
