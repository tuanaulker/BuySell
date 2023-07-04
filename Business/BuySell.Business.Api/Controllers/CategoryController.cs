using BuySell.Business.Application.Features.Category;
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
    public class CategoryController : ControllerBase
    {
        private IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryCommandRequest categoryCommandRequest)
        {
            ActionResponse<Categories> response = await _mediator.Send(categoryCommandRequest);
            response.IsSuccessful = true;
            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResponse<Categories>> UpdateCategory(UpdateCategoryCommandRequest updateCategoryRequest)
        {
            ActionResponse<Categories> response = await _mediator.Send(updateCategoryRequest);
            return response;
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResponse<Categories>> DeleteCategory(DeleteCategoryCommandRequest deleteCategoryRequest)
        {
            ActionResponse<Categories> response = await _mediator.Send(deleteCategoryRequest);
            return response;
        }
    }
}
