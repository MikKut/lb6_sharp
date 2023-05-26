using lb6_server.Models.Dto;
using lb6_server.Models.Responses;

namespace lb6_server.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<Guid> Login(UserDto user);
        Task<Guid> Register(UserDto user);
    }
}