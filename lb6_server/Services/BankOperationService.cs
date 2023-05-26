using AutoMapper;
using Infrastracture.Exceptions;
using lb6_server.Models.Dto;
using lb6_server.Models.Entity;
using lb6_server.Models.Requests;
using lb6_server.Models.Responses;
using lb6_server.Repositories;
using lb6_server.Repositories.Interfaces;
using lb6_server.Services.Interfaces;

namespace lb6_server.Services
{
    public class BankOperationService
        : BaseDataService<ApplicationDbContext>, IBankOperationService
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IMapper _mapper;
        public BankOperationService(IDbContextWrapper<ApplicationDbContext> dbContextWrapper, IIdentityRepository identityRepository, IMapper mapper)
            : base(dbContextWrapper)
        {
            _identityRepository = identityRepository;
            _mapper = mapper;
        }

        public async Task<bool> Withdraw(MoneyOperationDto moneyOperation)
        {
            var id = new Guid(moneyOperation.Id);
            var user = await _identityRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                throw new BusinessException("There is no such user");
            }

            if (user.Account < moneyOperation.Amount)
            {
                throw new BusinessException("there is no that much money");
            }

            return await ExecuteSafeAsync(() => _identityRepository.UpdateUserAccountAsync(user.Id, user.Account - moneyOperation.Amount));
        }

        public async Task<bool> Deposit(MoneyOperationDto moneyOperation)
        {
            var id = new Guid(moneyOperation.Id);
            var user = await _identityRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return false;
            }

            return await ExecuteSafeAsync(() => _identityRepository.UpdateUserAccountAsync(user.Id, user.Account + moneyOperation.Amount));
        }

        public async Task SendMoney(SendMoneyRequest request)
        {
            var initialId = new Guid(request.InititalUserId);
            var requestedId = new Guid(request.RequestUserId);
            var initialUser = await _identityRepository.GetUserByIdAsync(initialId);
            var guestUser = await _identityRepository.GetUserByIdAsync(requestedId);
            if (initialUser == null || guestUser == null)
            {
                throw new BusinessException("Id error: there is no such user.");
            }

            var withdrawResult = await Withdraw(new MoneyOperationDto() { Id = initialUser.Id.ToString(), Amount = request.Amount });
            if (!withdrawResult)
            {
                throw new BusinessException("Cannot withdraw money");
            }

            var depositResult = await Deposit(new MoneyOperationDto() { Id = guestUser.Id.ToString(), Amount = request.Amount });

            if (!depositResult)
            {
                bool rollbackResult = false;
                while (!rollbackResult)
                {
                    var xrollbackResult = (await Deposit(new MoneyOperationDto() { Id = initialUser.Id.ToString(), Amount = request.Amount }));
                }

                throw new BusinessException("Can't deposit money");
            }
        }

        public async Task<decimal> GetUserBalance(Guid id)
        {
            User? user = await ExecuteSafeAsync(() => _identityRepository.GetUserByIdAsync(id));
            if (user == null)
            {
                throw new BusinessException("There is no such user");
            }

            return user.Account;
        }

        public async Task<UserDto> GetUserByID(Guid id)
        {
            User? user = await ExecuteSafeAsync(() => _identityRepository.GetUserByIdAsync(id));
            if (user == null)
            {
                throw new BusinessException("There is no such user");
            }

            return _mapper.Map<UserDto>(user);
        }

        public async Task<Guid> GetUserId(UserDto user)
        {
             Guid? userId = await ExecuteSafeAsync(() => _identityRepository.GetUserIdAsync(user));
             if (userId == default(Guid))
             {
                 throw new BusinessException("There is no such user");
             }

            return (Guid)userId;
        }
    }
}
