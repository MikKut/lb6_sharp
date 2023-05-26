using AutoMapper;
using Infrastracture;
using lb6_server.Models.Dto;
using lb6_server.Models.Requests;
using lb6_server.Models.Responses;
using lb6_server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace lb6_server.Controllers
{
    [ApiController]
    [Route(ComponentDefaults.DefaultRoute)]
    public class BankingController : Controller
    {
        private readonly IBankOperationService _bankOperationService;
        private readonly IMapper _mapper;
        private readonly ILogger<BankingController> _logger;
        public BankingController(IBankOperationService bankOperationService, ILogger<BankingController> logger, IMapper mapper)
        {
            _logger = logger;
            _bankOperationService = bankOperationService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(GetBalanceResponse), (int)HttpStatusCode.OK)]
        public async Task<GetBalanceResponse> GetBalance(GetUserBalanceRequest request)
        {
            try
            {
                return new GetBalanceResponse()
                {
                    Balance = await _bankOperationService.GetUserBalance(new Guid(request.Id)),
                    Id = request.Id,
                    Result = new RequestResultDto() { IsSuccessful = true }
                };
            }
            catch (Exception ex) 
            {
                return new GetBalanceResponse()
                {
                    Result = new RequestResultDto() { IsSuccessful = false, Message = ex.Message }
                };
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(SuccessfulResultResponse), (int)HttpStatusCode.OK)]
        public async Task<SuccessfulResultResponse> Deposit(MoneyOperationDto request)
        {
            try
            {
                return new SuccessfulResultResponse() { Result = new RequestResultDto() { IsSuccessful = await _bankOperationService.Deposit(request) } };
            }
            catch (Exception ex)
            {
                return new SuccessfulResultResponse { Result = new RequestResultDto() { IsSuccessful = false, Message = ex.Message } };
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(SuccessfulResultResponse), (int)HttpStatusCode.OK)]
        public async Task<SuccessfulResultResponse> Withdraw(MoneyOperationDto request)
        {
            try
            {
                return new SuccessfulResultResponse() { Result = new RequestResultDto() { IsSuccessful = await _bankOperationService.Withdraw(request) } };
            }
            catch (Exception ex)
            {
                return new SuccessfulResultResponse { Result = new RequestResultDto() { IsSuccessful = false, Message = ex.Message } };
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserRequest), (int)HttpStatusCode.OK)]
        public async Task<SuccessfulResultResponseWithData<Guid>> GetUserId(UserRequest request)
        {
            try
            {
                return new SuccessfulResultResponseWithData<Guid>() { Data = await _bankOperationService.GetUserId(_mapper.Map<UserDto>(request)),  Result = new RequestResultDto() { IsSuccessful = true } };
            }
            catch (Exception ex)
            {
                return new SuccessfulResultResponseWithData<Guid> { Result = new RequestResultDto() { IsSuccessful = false, Message = ex.Message } };
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(SuccessfulResultResponse), (int)HttpStatusCode.OK)]
        public async Task<SuccessfulResultResponse> SendMoney(SendMoneyRequest request)
        {
            try
            {
                await _bankOperationService.SendMoney(request);
                return new SuccessfulResultResponse() { Result = new RequestResultDto() { IsSuccessful = true } };
            } 
            catch (Exception ex)
            {
                return new SuccessfulResultResponse { Result = new RequestResultDto() { IsSuccessful = false, Message = ex.Message } };
            }
        }
    }
}
