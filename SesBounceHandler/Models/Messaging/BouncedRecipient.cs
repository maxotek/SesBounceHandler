using Newtonsoft.Json;

namespace SesBounceHandler.Models.Messaging
{
    public class BouncedRecipient
    {
        [JsonProperty("emailAddress")]
        public string EmailAddress { get; set; } = null!;

        [JsonProperty("action")]
        public string Action { get; set; } = null!;

        [JsonProperty("status")]
        public string Status { get; set; } = null!;

        [JsonProperty("diagnosticCode")]
        public string DiagnosticCode { get; set; } = null!;
    }
}