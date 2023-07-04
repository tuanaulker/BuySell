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
    public class DeleteProductCommandRequest : IRequest<ActionResponse<Domain.Entities.Product>>
    {
        public string ProductId { get; set; }
    }

    public class DeleteProductCommand : IRequestHandler<DeleteProductCommandRequest, ActionResponse<Domain.Entities.Product>>
    {
        readonly BusinessDbContext _businessDbContext;

        public DeleteProductCommand(BusinessDbContext businessDbContext)
        {
            _businessDbContext = businessDbContext;
        }

        public async Task<ActionResponse<Domain.Entities.Product>> Handle(DeleteProductCommandRequest deleteProductRequest, CancellationToken cancellationToken)
        {
            ActionResponse<Domain.Entities.Product> response = new();
            response.IsSuccessful = false;

            Domain.Entities.Product product = await _businessDbContext.Products.FirstOrDefaultAsync(p => p.ProductId == new Guid(deleteProductRequest.ProductId));

            product.Status = false;
            await _businessDbContext.SaveChangesAsync();

            response.Data = product;
            response.IsSuccessful = true;

            return response;
        }
    }
}
