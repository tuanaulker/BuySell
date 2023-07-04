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

namespace BuySell.Business.Application.Features.Category
{
    public class CreateCategoryCommandRequest :IRequest<ActionResponse<Categories>>
    {
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
    }

    public class CreateCategoryCommand : IRequestHandler<CreateCategoryCommandRequest, ActionResponse<Categories>>
    {
        readonly BusinessDbContext _dbContext;
        readonly IUserInfoRepository _userInfoRepository;

        public CreateCategoryCommand(BusinessDbContext dbContext, IUserInfoRepository userInfoRepository)
        {
            _dbContext = dbContext;
            _userInfoRepository = userInfoRepository;
        }

        public async Task<ActionResponse<Categories>> Handle(CreateCategoryCommandRequest createCategoryRequest, CancellationToken cancellationToken)
        {
            ActionResponse<Categories> response = new();
            response.IsSuccessful = false;
            Categories category = new();
            category.CategoryName = createCategoryRequest.CategoryName;
            category.Id = createCategoryRequest.CategoryId;
            category.UserId = _userInfoRepository.User.UserId;
            category.Status = true;

            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            response.Data = category;
            response.IsSuccessful = true;

            return response;
        }
    }
}
