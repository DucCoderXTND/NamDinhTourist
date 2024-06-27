using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using TND.Domain.Entities;
using TND.Domain.Interfaces.Persistence.Services;

namespace TND.Infrastructure.Services.Images
{
    public class CloudinaryImageService : IImageService
    {
        private static readonly string[] AllowedImageFormats = [".jpg", ".jpeg", ".png"];
        private readonly Cloudinary _cloudinary;
        public CloudinaryImageService(IOptions<CloudinaryConfig> config)
        {
            var acc = new Account
              (
                  config.Value.CloudName,
                  config.Value.ApiKey,
                  config.Value.ApiSecret
              );
            _cloudinary = new Cloudinary(acc);

        }
        public async Task DeleteAsync(Image image, CancellationToken cancellationToken = default)
        {
            var deleteParams = new DeletionParams(image.Format);
            await _cloudinary.DestroyAsync(deleteParams);
        }

        public async Task<Image> StoreAsync(IFormFile image, CancellationToken cancellationToken = default)
        {
            if (image is null || image.Length < 0) throw new Exception();

            var imageFormat = image.ContentType.Split('/')[1];

            if (!IsAllowedImageFormat(imageFormat))
                throw new ArgumentOutOfRangeException();

            var uploadResult = new ImageUploadResult();
            if (image.Length > 0)
            {
                using var stream = image.OpenReadStream();//mở luồng y/c để đọc tệp đã tải lên
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(image.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face"),
                    Folder = "dating-web"
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            var imageModel = new Image();

            imageModel.Format = uploadResult.PublicId;
            imageModel.Path = uploadResult.SecureUrl.AbsoluteUri;

            return imageModel;
        }

        private static bool IsAllowedImageFormat(string imageFormat) => AllowedImageFormats.Contains(imageFormat);
    }
}
