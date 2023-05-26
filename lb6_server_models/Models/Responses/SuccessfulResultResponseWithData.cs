using lb6_server.Models.Dto;
using System.Text.Json.Serialization;

namespace lb6_server.Models.Responses
{
    public class SuccessfulResultResponseWithData<T>
    {
        [JsonPropertyName("data")]
        public T Data { get; set; }
        [JsonPropertyName("result")]
        public RequestResultDto Result { get; set; }
    }
}
