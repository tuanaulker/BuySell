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

namespace BuySell.Business.Application.Features.Category
{
    public class UpdateCategoryCommandRequest : IRequest<ActionResponse<Categories>>
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
    public class UpdateCategoryCommand : IRequestHandler<UpdateCategoryCommandRequest, ActionResponse<Categories>>
    {
        readonly BusinessDbContext _businessDbContext;

        public UpdateCategoryCommand(BusinessDbContext businessDbContext)
        {
            _businessDbContext = businessDbContext;
        }

        public async Task<ActionResponse<Categories>> Handle (UpdateCategoryCommandRequest updateCategoryRequest, CancellationToken cancellationToken)
        {
            ActionResponse<Categories> response = new();
            response.IsSuccessful = false;
            /* await _businessDbContext.Products.FirstOrDefaultAsync(p => p.ProductId == new Guid(updateProductRequest.ProductId));*/
            Categories category = await _businessDbContext.Categories.FirstOrDefaultAsync(c => c.Id == updateCategoryRequest.CategoryId);
            category.CategoryName = updateCategoryRequest.CategoryName;

            await _businessDbContext.SaveChangesAsync();
            response.Data = category;
            response.IsSuccessful = true;

            return response;
        }
    }
}
