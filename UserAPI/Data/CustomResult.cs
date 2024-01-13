using System.Text.Json.Serialization;

namespace UserAPI.Data
{
    public class CustomResult
    {
        public CustomResult(bool succeeded, object? content = null, object? errors = null)
        {
            Errors = errors;
            Succeeded = succeeded;
            Content = content;
        }

        [JsonPropertyName("succeeded")]
        public bool Succeeded { get; private set; } = false;

        [JsonPropertyName("content")]
        public object? Content { get; private set; }

        [JsonPropertyName("errors")]
        public object? Errors { get; private set; }
    }
}
