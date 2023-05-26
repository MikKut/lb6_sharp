using lb6_server.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace lb6_server.Models.Responses
{
    public class SuccessfulResultResponse
    {
        [JsonPropertyName("result")]
       public RequestResultDto Result { get; set; }
    }
}
