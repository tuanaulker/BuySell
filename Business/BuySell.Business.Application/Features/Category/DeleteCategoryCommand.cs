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
    public class DeleteCategoryCommandRequest : IRequest<ActionResponse<Categories>>
    {
        public int CategoryId { get; set; }
    }

    public class DeleteCategoryCommand : IRequestHandler<DeleteCategoryCommandRequest, ActionResponse<Categories>>
    {
        readonly BusinessDbContext _businessDbContext;

        public DeleteCategoryCommand(BusinessDbContext businessDbContext)
        {
            _businessDbContext = businessDbContext;
        }

        public async Task<ActionResponse<Categories>> Handle(DeleteCategoryCommandRequest deleteCategoryRequest, CancellationToken cancellationToken)
        {
            ActionResponse<Categories> response = new();
            response.IsSuccessful = false;

            Categories category = await _businessDbContext.Categories.FirstOrDefaultAsync(c => c.Id == deleteCategoryRequest.CategoryId);
            category.Status = false;

            await _businessDbContext.SaveChangesAsync();
            response.Data = category;
            response.IsSuccessful = true ;

            return response;
        }
    }
}
