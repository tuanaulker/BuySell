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

namespace BuySell.Business.Application.Features.ImageFile.DeleteImageFile
{
    public class DeleteImageFileCommandRequest : IRequest<ActionResponse<ProductImageFile>>
    {
        public string ImageId { get; set; }   //todo belki product id gelebilir
    }
    public class DeleteImageFileCommand : IRequestHandler<DeleteImageFileCommandRequest, ActionResponse<ProductImageFile>>
    {
        readonly BusinessDbContext _businessDbContext;

        public DeleteImageFileCommand(BusinessDbContext businessDbContext)
        {
            _businessDbContext = businessDbContext;
        }

        public async Task<ActionResponse<ProductImageFile>> Handle(DeleteImageFileCommandRequest deleteImageFile, CancellationToken cancellationToken)
        {
            ActionResponse<ProductImageFile> response = new();
            response.IsSuccessful= false;

            ProductImageFile imageFile = (ProductImageFile)await _businessDbContext.Files.FirstOrDefaultAsync(f => f.Id == new Guid(deleteImageFile.ImageId));
            imageFile.Status = false;
            await _businessDbContext.SaveChangesAsync();
            response.IsSuccessful= true;
            response.Data = imageFile;
            
            return response;
        }
    }
}
