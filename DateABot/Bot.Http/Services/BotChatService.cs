using Bot.Http.Responses;
using Domain.BotChats;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Bot.Http.Services
{
    public class BotChatService : IBotChatService
    {
        private readonly HttpClient _httpClient;

        // URL do serviço Langflow - pode ser movido para uma configuração externa
        private const string Url = "http://127.0.0.1:7860/api/v1/run/8c9d131d-303a-43ea-bb70-a1102ba2ded1?stream=false";

        public BotChatService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> Send(BotChatDto botChat, string message)
        {
            var requestBody = new
            {
                input_value = message,
                output_type = "chat",
                input_type = "chat",
                tweaks = new
                {
                    ChatInput_o1Yil = new { },
                    Prompt_6zqWn = new { },
                    Memory_qXjpa = new { },
                    CrewAIAgentComponent_dstlF = new { },
                    SequentialTaskComponent_YAzo4 = new { },
                    SequentialCrewComponent_yXgJV = new { },
                    ChatOutput_ftmrf = new { },
                    OllamaModel_bvaI9 = new { },
                    TextInput_iuHzk = new { input_value = botChat.Backstory },
                    TextInput_NvF1W = new { input_value = botChat.Role },
                    TextInput_9PHp7 = new { input_value = botChat.Goal }
                }
            };

            var jsonContent = JsonConvert.SerializeObject(requestBody);

            try
            {
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(Url, content);

                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<Response>(responseBody);

                if (responseObject?.Outputs?.Count > 0 && responseObject.Outputs[0].OutputsDetails.Count > 0)
                {
                    return responseObject.Outputs[0].OutputsDetails[0].Results.Message.Data.Text;
                }
                else
                {
                    throw new Exception("No outputs found in the response.");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"HTTP Request error: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while processing the request: {ex.Message}", ex);
            }
        }
    }
}
