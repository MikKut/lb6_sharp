using lb6_server.Models.Dto;
using lb6_server.Models.Entity;
using lb6_server.Models.Responses;

namespace lb6_server.Repositories.Interfaces
{
    public interface IIdentityRepository
    {
        Task<User?> GetUserByIdAsync(Guid id);
        Task<Guid?> GetUserIdAsync(UserDto user);
        Task<Guid> AddUserAsync(UserDto user);
        Task<User?> FindUserByNameAndPasswordAsync(UserDto user);
        Task<bool> UpdateUserAccountAsync(Guid userId, decimal newAccount);
    }
}