using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace lb6_server.Models.Dto
{
    public class RequestResultDto
    {
        [JsonPropertyName("message")]
        public string? Message { get; set; }
        [JsonPropertyName("issuccessful")]
        public bool IsSuccessful { get; set; }
        public override string ToString()
        {
            return IsSuccessful ? $"Result is successful" : $"Result is unsuccessful: {Message}";
        }
    }
}
