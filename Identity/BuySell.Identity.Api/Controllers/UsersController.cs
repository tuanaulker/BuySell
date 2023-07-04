
using BuySell.CommonModels;
using BuySell.Identity.Application.Features.Users.Commands.CreateUser;
using BuySell.Identity.Application.Features.Users.Commands.GetUser;
using BuySell.Identity.Application.Features.Users.Commands.LoginUser;
using BuySell.Identity.Application.Models;
using BuySell.Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuySell.Identity.Api.Controllers
{
    
    [Route("[controller]/[action]")]
    //[Route("/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
     
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        {
            CreateUserCommandResponse response = await _mediator.Send(createUserCommandRequest);
            return Ok(response);
        }

  
        [HttpPost]
        public async Task<IActionResult> LoginUser(LoginUserCommandRequest loginUserCommandRequest)
        {
            ActionResponse<TokenDto> token = await _mediator.Send(loginUserCommandRequest);
            return Ok(token);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserById([FromBody]GetUserCommandRequest getUserCommandRequest)
        {
            ActionResponse<AppUser> user = await _mediator.Send(getUserCommandRequest);
            return Ok(user);
        }


        
    }
}
