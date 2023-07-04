using BuySell.Business.Application.Features.ImageFile.DeleteImageFile;
using BuySell.Business.Application.Features.ImageFile.UploadImageFile;
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
    public class ImageFileController : ControllerBase
    {
        //readonly IWebHostEnvironment _webHostEnvironment;
        private IMediator _mediator;

        public ImageFileController( IMediator mediator)
        {
            _mediator = mediator;
        }


        [Authorize]
        [HttpPost]
       // public async Task<ActionResponse<ProductImageFile>> Upload([FromQuery] string id, [FromForm] IFormFileCollection files)
       // inside the request give it with manuel Files = file, ProductId Id
        public async Task<ActionResponse<ProductImageFile>> Upload([FromForm]UploadImageFileCommandRequest uploadImageFileCommandRequest)
        {
            uploadImageFileCommandRequest.Files = Request.Form.Files;
            ActionResponse<ProductImageFile> response = await _mediator.Send(uploadImageFileCommandRequest);
            return response;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResponse<ProductImageFile>> Delete([FromBody]DeleteImageFileCommandRequest deleteImageFile)
        {
            ActionResponse<ProductImageFile> response = await _mediator.Send(deleteImageFile);
            return response;
        }
    }
}










//product id ile görsel eklenmeli, eğer varsa numara ile gitmeli id-1, id-2...

/*[HttpPost]
public async Task<IActionResult> UploadAsync()
{
    //wwwroot/resource/product-images
    string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "resource/product-images");

    if (!Directory.Exists(uploadPath))
        Directory.CreateDirectory(uploadPath);

    Random r = new();
    foreach (IFormFile file in Request.Form.Files)
    {
        string fullPath = Path.Combine(uploadPath, $"{r.Next()}{Path.GetExtension(file.FileName)}");

        using FileStream fileStream = new(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
        await file.CopyToAsync(fileStream);
        await fileStream.FlushAsync();
    }
    return Ok();
}*/