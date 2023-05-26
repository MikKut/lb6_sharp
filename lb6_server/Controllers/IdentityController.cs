using Infrastracture;
using lb6_server.Models.Dto;
using lb6_server.Models.Responses;
using lb6_server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace lb6_server.Controllers
{
    [ApiController]
    [Route(ComponentDefaults.DefaultRoute)]
    public class IdentityController : Controller
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService) 
        {
            _identityService = identityService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(SuccessfulResultResponse), (int)HttpStatusCode.OK)]
        public async Task<SuccessfulResultResponseWithData<Guid>> Register(UserDto user)
        {
            try
            {
                var id = await _identityService.Register(user);
                return new SuccessfulResultResponseWithData<Guid>() { Data = id, Result = new RequestResultDto() { IsSuccessful = true } };
            }
            catch (Exception ex) 
            {
                return new SuccessfulResultResponseWithData<Guid>() { Result = new RequestResultDto() { IsSuccessful = false, Message = ex.Message } };
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(SuccessfulResultResponse), (int)HttpStatusCode.OK)]
        public async Task<SuccessfulResultResponseWithData<Guid>> Login(UserDto user)
        {
            try
            {
                var id = await _identityService.Login(user);
                return new SuccessfulResultResponseWithData<Guid>() { Data = id, Result = new RequestResultDto() { IsSuccessful = true } };
            }
            catch (Exception ex)
            {
                return new SuccessfulResultResponseWithData<Guid>() { Result = new RequestResultDto() { IsSuccessful = false, Message = ex.Message } };
            }
        }
    }
}
