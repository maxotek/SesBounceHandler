using Newtonsoft.Json;

namespace SesBounceHandler.Models.Messaging
{
    public class SesEmailEvent
    {
        [JsonProperty("eventType")]
        public string EventType { get; set; }

        [JsonProperty("mail")]
        public Mail Mail { get; set; }

        [JsonProperty("delivery")]
        public Delivery Delivery { get; set; }

        [JsonProperty("bounce")]
        public Bounce Bounce { get; set; }
    }
}