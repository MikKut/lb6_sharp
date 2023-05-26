using Infrastracture.Exceptions;
using lb6_server.Models.Dto;
using lb6_server.Models.Entity;
using lb6_server.Models.Responses;
using lb6_server.Repositories.Interfaces;
using lb6_server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lb6_server.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public IdentityRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> FindUserByNameAndPasswordAsync(UserDto user)
        {
            return await _dbContext.Users.Where(x => x.Name == user.Name).Where(x => x.Password == user.Password).SingleOrDefaultAsync();
        }

        public async Task<Guid> AddUserAsync(UserDto user)
        {
            if (await FindUserByNameAndPasswordAsync(user) != null)
            {
                throw new BusinessException("Can't add the user: there is the user already in database. ");
            }

            var id = Guid.NewGuid();
            var operationResult = await _dbContext.Users.AddAsync(new User() { Name = user.Name, Password = user.Password, Account = 0, Id = id });
            await _dbContext.SaveChangesAsync();
            return id;
        }

        public async Task<Guid?> GetUserIdAsync(UserDto user)
        {
            var count = await _dbContext.Users.CountAsync();
            var result = await _dbContext.Users.Where(x => x.Name == user.Name).Where(x => x.Password == user.Password).SingleOrDefaultAsync();
            return result.Id;
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            var result = await _dbContext.Users.Where(x => x.Id == id).SingleOrDefaultAsync();
            return result;
        }

        public async Task<bool> UpdateUserAccountAsync(Guid userId, decimal newAccount)
        {
            try
            {
                var existingUser = await GetUserByIdAsync(userId);

                if (existingUser != null)
                {
                    existingUser.Account = newAccount;
                    await _dbContext.SaveChangesAsync();
                    return true;
                }

                throw new BusinessException("There is no such user");
            }
            catch (Exception) 
            {
                return false;
            }
        }
    }
}
