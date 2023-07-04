using BuySell.Business.Application.Features.UserInfos.Commands.CreateUserInfo;
using BuySell.Business.Domain.Entities;
using BuySell.CommonModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuySell.Business.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private IMediator _mediator;

        public UserInfoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateUserInfo(CreateUserInfoCommandRequest userInfoCommandRequest)
        {
            ActionResponse<UserInfo> response = await _mediator.Send(userInfoCommandRequest);
            return Ok(response);
        }

    }
}
