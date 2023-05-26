using System.Xml.Serialization;

namespace Infrastracture
{
    public interface IInternalHttpService
    {
        Task<TResponse> SendRequest<TSerializer, TRequest, TResponse>(string serverUrl, string action, HttpMethod method, TSerializer serializer, TRequest? request) where TSerializer : XmlSerializer;
    }
}