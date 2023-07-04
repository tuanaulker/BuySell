using BuySell.CommonModels;
using BuySell.Identity.Application.Features.Token.Queries;
using BuySell.Identity.Application.Models;
using BuySell.Identity.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BuySell.Identity.Application.Features.Token.Commands
{
   
    public class TokenCommandRequest : IRequest<ActionResponse<TokenDto>>
    {
        public AppUser User { get; set; }
    }

    public class TokenCommand : IRequestHandler<TokenCommandRequest, ActionResponse<TokenDto>>
    {
        readonly IConfiguration _configuration;

        public TokenCommand(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<ActionResponse<TokenDto>> Handle(TokenCommandRequest tokenCommandRequest, CancellationToken cancellationToken)
        {
            ActionResponse<TokenDto> response = new();
            TokenDto token = new();
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("UserId", tokenCommandRequest.User.Id),
                new Claim("UserName", tokenCommandRequest.User.UserName),
                new Claim("Email", tokenCommandRequest.User.Email)
            };
            
            var date = DateTime.UtcNow;
            token.Expiration = date.AddDays(1);
            

            JwtSecurityToken tokenSecurityToken = new(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                expires: token.Expiration,
                notBefore: date,
                signingCredentials: signingCredentials,
                claims: claims
                );
            
            JwtSecurityTokenHandler tokenHandler= new();
            token.AccessToken = tokenHandler.WriteToken(tokenSecurityToken);

            response.Data = token;
            response.IsSuccessful = true;

            return response;
        }
    }

    
}
