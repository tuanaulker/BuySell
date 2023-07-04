
using BuySell.Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Identity.Application.Features.Users.Commands.CreateUser
{

    public class CreateUserCommandRequest : IRequest<CreateUserCommandResponse>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //public string? PasswordConfirm { get; set; }
    }

   
    public class CreateUserCommand : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly UserManager<AppUser> _userManager;

        public CreateUserCommand(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = request.UserName,
                Email = request.Email,
            }, request.Password);

            CreateUserCommandResponse response = new()
            {
                IsSuccessful = result.Succeeded,
            };

            if (result.Succeeded)
                response.Message = "User created.";
            else
            {
                foreach (var error in result.Errors)
                    response.Message += $"{error.Code} - {error.Description}\n";
            }
            return response;



            //throw new UserCreateFailedException();


        }
    }
}
