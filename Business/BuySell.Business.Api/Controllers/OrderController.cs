using BuySell.Business.Application.Features.Order;
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
    public class OrderController : ControllerBase
    {
        readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResponse<Orders>> CreateOrder(CreateOrderCommandRequest createOrderRequest)
        {
            ActionResponse<Orders> response = await _mediator.Send(createOrderRequest);
            return response;

        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResponse<Orders>> UpdateOrder(UpdateOrderCommandRequest updateOrderRequest)
        {
            ActionResponse<Orders> response = await _mediator.Send(updateOrderRequest);
            return response;
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResponse<Orders>> DeleteOrder(DeleteOrderCommandRequest deleteOrderRequest)
        {
            ActionResponse<Orders> response = await _mediator.Send(deleteOrderRequest);
            return response;
        }






            
    }
}
