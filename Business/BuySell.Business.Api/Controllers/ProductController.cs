using BuySell.Business.Application.Features.Product;
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
    public class ProductController : ControllerBase
    {
        private IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommandRequest productCommandRequest)
        {
            ActionResponse<Product> response = await _mediator.Send(productCommandRequest);
            return Ok(response);
        }

        [Authorize]
        [HttpPost] //fark etmezmiş :)
        public async Task<ActionResponse<Product>> UpdateProduct(UpdateProductCommandRequest updateProductRequest)
        {
            ActionResponse<Product> response = await _mediator.Send(updateProductRequest);
            return response;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResponse<Product>> DeleteProduct(DeleteProductCommandRequest deleteProductRequest)
        {
            ActionResponse<Product> response = await _mediator.Send(deleteProductRequest);
            return response;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {

            return Ok();

        }

    }
}

