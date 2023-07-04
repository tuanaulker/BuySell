using BuySell.Business.Application.Abstraction.Storage;
using BuySell.Business.Application.Repositories;
using BuySell.Business.Domain.Entities;
using BuySell.Business.Infrastructure.Persistence;
using BuySell.CommonModels;
using BuySell.CommonModels.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Business.Application.Features.ImageFile.UploadImageFile
{
    public class UploadImageFileCommandRequest  : IRequest<ActionResponse<ProductImageFile>>
    {
        public string ProductId { get; set; }
        public IFormFileCollection? Files { get; set; }

    }


    public class UploadImageFileCommand : IRequestHandler<UploadImageFileCommandRequest,  ActionResponse<ProductImageFile>>
    {
        readonly IStorageService _storageService;
        //readonly IProductReadRepository _productReadRepository;
        // readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        readonly IUserInfoRepository _userInfoRepository;
        readonly BusinessDbContext _businessDbContext;

        public UploadImageFileCommand(IStorageService storageService,  IUserInfoRepository userInfoRepository, BusinessDbContext businessDbContext)
        {
            _storageService = storageService;
            //_productReadRepository = productReadRepository;
            //_productImageFileWriteRepository = productImageFileWriteRepository;
            _userInfoRepository = userInfoRepository;
            _businessDbContext = businessDbContext;
        }

        public async Task<ActionResponse<ProductImageFile>> Handle(UploadImageFileCommandRequest uploadImageFileRequest, CancellationToken cancellationToken)
        {
            ActionResponse<ProductImageFile> response = new();
            response.IsSuccessful = false;
          
           
            List<(string fileName, string pathOrContainer)> result = await _storageService.UploadAsync("photo-images", uploadImageFileRequest.Files);
            //Domain.Entities.Product product = await _productReadRepository.GetByIdAsync(uploadImageFileRequest.ProductId);

           
   
            

            foreach (var item in result)
            {
                ProductImageFile imageFile = new();
                imageFile.FileName = item.fileName;
                imageFile.Path = item.pathOrContainer;
                imageFile.Storage = _storageService.StorageName;
                imageFile.ProductId = new Guid(uploadImageFileRequest.ProductId);
                imageFile.Id = Guid.NewGuid();
                imageFile.UserId = _userInfoRepository.User.UserId;
                imageFile.Status = true;
                //datas.Add(imageFile);
                await _businessDbContext.Files.AddAsync(imageFile);
            
      
            }

            await _businessDbContext.SaveChangesAsync();
            //response.Data = datas;
            response.IsSuccessful = true;
            return response;

            //await _productImageFileWriteRepository.AddRangeAsync(result.Select(p => new ProductImageFile
            //{
            //    FileName = p.fileName,
            //    Path = p.pathOrContainer,
            //    Storage = _storageService.StorageName,
            //    // Products = new List<Domain.Entities.Product>() { product }

            //}).ToList());

            //await _productImageFileWriteRepository.SaveAsync();



        }

       
    }
    
}
