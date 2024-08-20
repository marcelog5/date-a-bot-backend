using Newtonsoft.Json;

namespace Bot.Http.Responses
{
    internal sealed class Logs
    {
        [JsonProperty("message")]
        public List<object> Message { get; set; } // Assuming logs are a list of objects
    }
}
