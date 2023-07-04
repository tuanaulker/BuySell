using Azure.Core;
using BuySell.Business.Domain.Entities;
using BuySell.Business.Infrastructure.Persistence;
using BuySell.CommonModels;
using BuySell.CommonModels.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Business.Application.Features.UserInfos.Commands.CreateUserInfo
{
    
    public class CreateUserInfoCommandRequest : IRequest<ActionResponse<UserInfo>>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }

    public class CreateUserInfoCommand : IRequestHandler<CreateUserInfoCommandRequest, ActionResponse<UserInfo>>
    {
        readonly BusinessDbContext _dbContext;
        readonly IUserInfoRepository _userInfoRepository;
        
            
        public CreateUserInfoCommand(BusinessDbContext dbContext, IUserInfoRepository userInfoRepository)
        {
            _dbContext= dbContext;
            _userInfoRepository= userInfoRepository;
        }

        public async Task<ActionResponse<UserInfo>> Handle(CreateUserInfoCommandRequest createUserInfoCommandRequest, CancellationToken cancellationToken)
        {
            ActionResponse<UserInfo> response = new();
            UserInfo user = new();
            user.Id = Guid.NewGuid();
            user.UserId = _userInfoRepository.User.UserId;
            user.Name = createUserInfoCommandRequest.Name;
            user.Surname = createUserInfoCommandRequest.Surname;
            user.Address = createUserInfoCommandRequest.Address;
            user.Phone= createUserInfoCommandRequest.Phone;
           

            await _dbContext.UserInfos.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            response.Data = user;
            response.IsSuccessful= true;

            return response;
        }    
    }
}
