using Newtonsoft.Json;

namespace SesBounceHandler.Models.Messaging
{
    public class Header
    {
        [JsonProperty("name")]
        public string Name { get; set; } = null!;

        [JsonProperty("value")]
        public string Value { get; set; } = null!;
    }
}