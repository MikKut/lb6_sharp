using lb6_server.Models.Dto;
using lb6_server.Models.Entity;
using lb6_server.Models.Requests;
using lb6_server.Models.Responses;

namespace lb6_server.Services.Interfaces
{
    public interface IBankOperationService
    {
        Task<bool> Deposit(MoneyOperationDto moneyOperation);
        Task<decimal> GetUserBalance(Guid id);
        Task<UserDto> GetUserByID(Guid id);
        Task<Guid> GetUserId(UserDto user);
        Task SendMoney(SendMoneyRequest request);
        Task<bool> Withdraw(MoneyOperationDto moneyOperation);
    }
}