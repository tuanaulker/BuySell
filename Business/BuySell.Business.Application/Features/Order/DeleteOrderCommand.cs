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
    public class DeleteOrderCommandRequest : IRequest<ActionResponse<Orders>>
    {
        public Guid OrderId { get; set; }
    }
    
    public class DeleteOrderCommand : IRequestHandler<DeleteOrderCommandRequest, ActionResponse<Orders>>
    {
        readonly BusinessDbContext _businessDbContext;

        public DeleteOrderCommand(BusinessDbContext dbContext)
        {
            _businessDbContext = dbContext;
        }

        public async Task<ActionResponse<Orders>> Handle(DeleteOrderCommandRequest deleteOrderRequest, CancellationToken cancellationToken)
        {
            ActionResponse<Orders> response = new();
            response.IsSuccessful = false;

            Orders order = await _businessDbContext.Orders.FirstOrDefaultAsync(d => d.Id == deleteOrderRequest.OrderId);
            if (order != null)
            { 
                order.Status = false;
                await _businessDbContext.SaveChangesAsync();
                response.Data = order;
                response.IsSuccessful = true;
            }
            return response;
        }
    }
}
