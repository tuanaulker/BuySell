using BuySell.Identity.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Identity.Application.Features.Users.Commands.LoginUser
{
    public class LoginUserCommandResponse
    {

    }
    public class LoginUserCommandSuccessResponse : LoginUserCommandResponse
    {
        public TokenDto Token { get; set; }
    }
}
