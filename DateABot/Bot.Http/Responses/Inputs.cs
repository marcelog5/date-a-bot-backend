using Newtonsoft.Json;

namespace Bot.Http.Responses
{
    internal sealed class Inputs
    {
        [JsonProperty("input_value")]
        public string InputValue { get; set; }
    }
}
