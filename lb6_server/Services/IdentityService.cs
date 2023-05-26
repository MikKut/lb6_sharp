using lb6_server.Models.Dto;
using lb6_server.Models.Responses;
using lb6_server.Repositories.Interfaces;
using lb6_server.Repositories;
using lb6_server.Services.Interfaces;
using Infrastracture.Exceptions;

namespace lb6_server.Services
{
    public class IdentityService
        : BaseDataService<ApplicationDbContext>, IIdentityService
    {
        private readonly IIdentityRepository _identityRepository;
        public IdentityService(IDbContextWrapper<ApplicationDbContext> dbContextWrapper, IIdentityRepository identityRepository)
            : base(dbContextWrapper)
        {
            _identityRepository = identityRepository;
        }

        public async Task<Guid> Login(UserDto user)
        {
            var result = await _identityRepository.FindUserByNameAndPasswordAsync(user);
            if (result != null)
            {
                return result.Id;
            }

            throw new BusinessException("There is no such user");
        }

        public async Task<Guid> Register(UserDto user)
        {
            var result = await _identityRepository.FindUserByNameAndPasswordAsync(user);
            if (result == null)
            {
                var id = await _identityRepository.AddUserAsync(user);
                return id;
            }

            throw new BusinessException("There is a user exist already");
        }
    }
}
