using BuySell.Business.Domain.Entities;
using BuySell.Business.Infrastructure.Persistence;
using BuySell.CommonModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Business.Application.Features.Order
{
    public class UpdateOrderCommandRequest : IRequest<ActionResponse<Orders>>
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set;}
    }

    public class UpdateOrderCommand : IRequestHandler<UpdateOrderCommandRequest, ActionResponse<Orders>>
    {
        readonly BusinessDbContext _businessDbContext;

        public UpdateOrderCommand(BusinessDbContext businessDbContext)
        {
            _businessDbContext = businessDbContext;
        }

        public async Task<ActionResponse<Orders>> Handle(UpdateOrderCommandRequest updateOrderRequest, CancellationToken cancellationToken)
        {
            ActionResponse<Orders> response = new();
            response.IsSuccessful = false;
            Orders order = await _businessDbContext.Orders.FirstOrDefaultAsync(d => d.Id == updateOrderRequest.OrderId);
            if (order != null && order.Status == true)
            {
                order.ProductId= updateOrderRequest.ProductId;
                await _businessDbContext.SaveChangesAsync();
                response.Data = order;
                response.IsSuccessful = true;
            }

            return response;
        }
    }
}
