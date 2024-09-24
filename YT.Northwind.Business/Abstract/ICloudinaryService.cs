

using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Northwind.Core.Models.Response.Cloudinary;

namespace Northwind.Business.Abstract
{
    public interface ICloudinaryService
    {

        public Task<DelResResult> DeleteImageAsync(string publicId);
        public Task<List<UploadImageResponseModel>> UploadImageAsync(IFormFile[] images, string folderName);

    }
}
