using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Infrastracture
{
    public class InternalHttpService : IInternalHttpService
    {
        public async Task<TResponse> SendRequest<TSerializer, TRequest, TResponse>(string serverUrl, string action, HttpMethod method, TSerializer serializer, TRequest? request)
    where TSerializer : XmlSerializer
        {
            try
            {
                HttpClient client = new HttpClient();
                string serializedData;

                HttpRequestMessage httpMessage = new()
                {
                    RequestUri = new Uri($@"{serverUrl}/{action}"),
                    Method = method
                };

                if (request != null)
                {
                    httpMessage.Content =
                        new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
                }

                HttpResponseMessage result = await client.SendAsync(httpMessage);
                string responseXml = await result.Content.ReadAsStringAsync();

                if (result.IsSuccessStatusCode)
                {
                    TResponse? response = JsonSerializer.Deserialize<TResponse>(responseXml);
                    return response!;
                }

                return default!;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return default!;
            }
        }
    }
}
