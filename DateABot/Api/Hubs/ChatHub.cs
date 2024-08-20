using Microsoft.AspNetCore.SignalR;

namespace Api.Hubs
{
    public class ChatHub : Hub
    {
        // Mapeia o ConnectionId para o userId
        private static Dictionary<int, string> userConnections = new Dictionary<int, string>();

        public override Task OnConnectedAsync()
        {
            // Assumindo que você passa o ID do usuário no contexto de conexão
            var userId = int.Parse(Context.GetHttpContext().Request.Query["userId"]);

            // Adiciona ou atualiza a conexão do usuário
            userConnections[userId] = Context.ConnectionId;

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            // Remove a conexão do usuário quando ele se desconecta
            var userId = GetUserIdByConnectionId(Context.ConnectionId);
            if (userId.HasValue)
            {
                userConnections.Remove(userId.Value);
            }

            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string user, string message)
        {
            // Busca o userId do remetente (extraído do nome do usuário)
            var userId = int.Parse(user.Replace("User", ""));
            
            if (userConnections.TryGetValue(userId, out string connectionId))
            {
                // Envia a mensagem original para todos os clientes
                await Clients.Client(connectionId).SendAsync("ReceiveMessage", user, message);

                // Gera um número aleatório
                var random = new Random();
                int randomNumber = random.Next(1, 100); // Exemplo: números de 1 a 100


                // Encontra o ConnectionId do userId e envia a resposta
                // Responde ao cliente específico com o número aleatório
                await Clients.Client(connectionId).SendAsync("ReceiveMessage", "Servidor", $"Número aleatório: {randomNumber}");
            }
        }

        // Helper method to get userId by connectionId
        private int? GetUserIdByConnectionId(string connectionId)
        {
            foreach (var pair in userConnections)
            {
                if (pair.Value == connectionId)
                {
                    return pair.Key;
                }
            }
            return null;
        }
    }
}
