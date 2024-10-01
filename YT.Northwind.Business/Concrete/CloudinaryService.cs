
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Northwind.Business.Abstract;
using Northwind.Core.Models.Response.Cloudinary;

namespace Northwind.Business.Concrete
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;
        public CloudinaryService()
        {
           var cloudinary = new Cloudinary("cloudinary://542582965683941:NDAIO5S29woloyXfOzN0BegpnIM@dgkkqlh2c");
            cloudinary.Api.Secure = true;
            _cloudinary = cloudinary;

        }
        public async Task<DelResResult> DeleteImageAsync(string publicId)
        {
            try
            {
                var deletedResult = await _cloudinary.DeleteResourcesAsync(publicId);
                return deletedResult;

            }catch (Exception ex)
            {
                return new DelResResult
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Error = new Error { Message = ex.Message }


                };
            }
        }

        public async Task<List<UploadImageResponseModel>> UploadImageAsync(IFormFile[] images, string folderName)
        {
            
            var uploadImages = new List<UploadImageResponseModel>();
            foreach (var image in images)
            {
                var uploadParams = new ImageUploadParams();

                using var stream = image.OpenReadStream();
                var uploadResult = await _cloudinary.UploadAsync(new ImageUploadParams()
                {
                    File = new FileDescription(image.FileName, stream),
                    Folder = folderName
                });

               
                uploadImages.Add(new UploadImageResponseModel
                {
                    PublicID = uploadResult.PublicId,
                    ImagePath = uploadResult.SecureUrl.ToString()
                });
            }
               return uploadImages;
        }
    }
}
