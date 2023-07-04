using BuySell.Business.Application.Repositories;
using BuySell.Business.Domain.Entities;
using BuySell.Business.Infrastructure.Persistence;
using BuySell.CommonModels;
using BuySell.CommonModels.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Business.Application.Features.Product
{
    public class CreateProductCommandRequest :  IRequest<ActionResponse<Domain.Entities.Product>>
    {
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public float ProductPrice { get; set; }
    }

    public class CreateProductCommand : IRequestHandler<CreateProductCommandRequest, ActionResponse<Domain.Entities.Product>>
    {
        // readonly IProductWriteRepository _productWriteRepository;
        readonly BusinessDbContext _dbContext;
        readonly IUserInfoRepository _userInfoRepository;

        public CreateProductCommand(BusinessDbContext dbContext, IUserInfoRepository userInfoRepository)
        {
            _dbContext = dbContext;
            _userInfoRepository = userInfoRepository;
        }

        public async Task<ActionResponse<Domain.Entities.Product>> Handle(CreateProductCommandRequest productRequest, CancellationToken cancellationToken)
        {
            ActionResponse<Domain.Entities.Product> response = new();
            response.IsSuccessful = false;
            Domain.Entities.Product product = new();
            product.ProductName = productRequest.ProductName;
            product.ProductDescription = productRequest.ProductDescription;
            product.ProductPrice = productRequest.ProductPrice;
            product.CategoryId = productRequest.CategoryId;
            product.Status = true;
            product.ProductId = Guid.NewGuid();
            product.UserId = _userInfoRepository.User.UserId;

            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            response.Data = product;
            response.IsSuccessful = true;

            return response;
        }
    }
       
}
