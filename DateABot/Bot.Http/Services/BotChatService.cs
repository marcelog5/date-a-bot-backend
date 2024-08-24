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
        private const string Url = "http://127.0.0.1:7860/api/v1/run/8c9d131d-303a-43ea-bb70-a1102ba2ded1";

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
                    CrewAIAgentComponent_dstlF = new
                    {
                        allow_code_execution = false,
                        allow_delegation = true,
                        backstory = botChat.Backstory,
                        goal = botChat.Goal,
                        kwargs = new { },
                        memory = true,
                        role = botChat.Role,
                        verbose = false
                    },
                    SequentialTaskComponent_YAzo4 = new
                    {
                        async_execution = true,
                        expected_output = "Personalized, flirtatious, and engaging messages tailored to the user's preferences, aiming to create a romantic and seductive interaction.",
                        task_description = ""
                    },
                    SequentialCrewComponent_yXgJV = new
                    {
                        max_rpm = 2,
                        memory = false,
                        share_crew = false,
                        use_cache = true,
                        verbose = 0
                    },
                    ChatOutput_ftmrf = new
                    {
                        data_template = "{text}",
                        input_value = "",
                        sender = "Machine",
                        sender_name = "AI",
                        session_id = "",
                        should_store_message = true
                    },
                    OllamaModel_IuJSA = new
                    {
                        base_url = "http://localhost:11434",
                        format = "",
                        input_value = "",
                        metadata = new { },
                        mirostat = "Disabled",
                        mirostat_eta = (object)null,
                        mirostat_tau = (object)null,
                        model_name = "reefer/monicamaxlvl:latest",
                        num_ctx = (object)null,
                        num_gpu = (object)null,
                        num_thread = (object)null,
                        repeat_last_n = (object)null,
                        repeat_penalty = (object)null,
                        stop_tokens = "",
                        stream = false,
                        system = "",
                        system_message = "",
                        tags = "",
                        temperature = 0.2,
                        template = "",
                        tfs_z = (object)null,
                        timeout = (object)null,
                        top_k = (object)null,
                        top_p = (object)null,
                        verbose = false
                    }
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
