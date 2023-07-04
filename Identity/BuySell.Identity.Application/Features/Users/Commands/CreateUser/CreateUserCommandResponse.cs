using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Identity.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandResponse
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
    }
}
