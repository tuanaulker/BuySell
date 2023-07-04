using BuySell.Business.Infrastructure.Persistence;
using BuySell.CommonModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Business.Application.Features.Product
{
    public class UpdateProductCommandRequest : IRequest<ActionResponse<Domain.Entities.Product>>
    {
        //public int CategoryId { get; set; }  unchangeable
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public float ProductPrice { get; set; }
    }

    public class UpdateProductCommand : IRequestHandler<UpdateProductCommandRequest, ActionResponse<Domain.Entities.Product>>
    {
        readonly BusinessDbContext _businessDbContext;

        public UpdateProductCommand(BusinessDbContext businessDbContext)
        {
            _businessDbContext = businessDbContext;
        }

        public async Task<ActionResponse<Domain.Entities.Product>> Handle(UpdateProductCommandRequest updateProductRequest, CancellationToken cancellationToken)
        {
            ActionResponse<Domain.Entities.Product> response = new();
            response.IsSuccessful = false;

            Domain.Entities.Product product = await _businessDbContext.Products.FirstOrDefaultAsync(p => p.ProductId == new Guid(updateProductRequest.ProductId));
            product.ProductName = updateProductRequest.ProductName;
            product.ProductDescription = updateProductRequest.ProductDescription;
            product.ProductPrice = updateProductRequest.ProductPrice;
            
            await _businessDbContext.SaveChangesAsync();
            response.Data = product;
            response.IsSuccessful = true;

            return response;
        }
    }
}
