using Bot.Http.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bot.Http.Helpers
{
    internal sealed class MessageDetailConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(MessageDetail);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);

            if (token.Type == JTokenType.String)
            {
                return new MessageDetail { Text = token.ToString() };
            }
            else if (token.Type == JTokenType.Object)
            {
                return token.ToObject<MessageDetail>();
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var messageDetail = value as MessageDetail;
            if (messageDetail != null)
            {
                writer.WriteValue(messageDetail.Text);
            }
        }
    }
}
