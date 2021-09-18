using Newtonsoft.Json;

namespace SesBounceHandler.Models.Messaging
{
    public class Tags
    {
        [JsonProperty("ses:operation")]
        public string[] SesOperation { get; set; }

        [JsonProperty("ses:configuration-set")]
        public string[] SesConfigurationSet { get; set; }

        [JsonProperty("ses:source-ip")]
        public string[] SesSourceIp { get; set; }

        [JsonProperty("ses:from-domain")]
        public string[] SesFromDomain { get; set; }

        [JsonProperty("ses:caller-identity")]
        public string[] SesCallerIdentity { get; set; }

        [JsonProperty("ses:outgoing-ip")]
        public string[] SesOutgoingIp { get; set; }
    }
}