using BuySell.Business.Domain.Entities;
using BuySell.Business.Infrastructure.Persistence;
using BuySell.CommonModels;
using BuySell.CommonModels.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Business.Application.Features.Order
{
    public class CreateOrderCommandRequest : IRequest<ActionResponse<Orders>>
    {
        public Guid ProductId { get; set; }
    }
   
    public class CreateOrderCommand : IRequestHandler<CreateOrderCommandRequest, ActionResponse<Orders>>
    {
        readonly BusinessDbContext _dbContext;
        readonly IUserInfoRepository _userInfoRepository;

        public CreateOrderCommand(BusinessDbContext dbContext, IUserInfoRepository userInfoRepository)
        {
            _dbContext = dbContext;
            _userInfoRepository = userInfoRepository;
        }

        public async Task<ActionResponse<Orders>> Handle(CreateOrderCommandRequest createOrderRequest, CancellationToken cancellationToken)
        {
            ActionResponse<Orders> response = new();
            response.IsSuccessful = false;

            Orders orders = new();
            orders.Id = Guid.NewGuid();
            orders.ProductId = createOrderRequest.ProductId;
            orders.UserId = _userInfoRepository.User.UserId;
            orders.UserInfoId = _dbContext.UserInfos.FirstOrDefaultAsync(p => p.UserId == orders.UserId).Result.Id;
            orders.Status = true;

            await _dbContext.Orders.AddAsync(orders);
            await _dbContext.SaveChangesAsync();

            response.Data = orders;
            response.IsSuccessful = true;

            return response;

        }
    }
}
