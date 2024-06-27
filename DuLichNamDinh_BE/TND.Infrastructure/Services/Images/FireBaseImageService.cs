using Firebase.Storage;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using TND.Domain.Entities;
using TND.Domain.Interfaces.Persistence.Services;

namespace TND.Infrastructure.Services.Images
{
    public class FireBaseImageService : IImageService
    {
        private static readonly string[] AllowedImageFormats = [".jpg", ".jpeg", ".png"];
        private readonly FireBaseConfig _fireBaseConfig;

        public FireBaseImageService(IOptions<FireBaseConfig> fireBaseConfig)
        {
            _fireBaseConfig = fireBaseConfig.Value;
        }
        public async Task<Image> StoreAsync(IFormFile image, CancellationToken cancellationToken = default)
        {
            if (image is null || image.Length < 0) throw new Exception();

            var imageFormat = image.ContentType.Split('/')[1];

            if (!IsAllowedImageFormat(imageFormat))
                throw new ArgumentOutOfRangeException();

            var credential = GoogleCredential.FromJson(_fireBaseConfig.Credentials);

            var storage = await StorageClient.CreateAsync(credential);

            var imageModel = new Image { Format = imageFormat };

            var destinationObjectName = $"{imageModel.Id}.{imageFormat}";

            await storage.UploadObjectAsync(
                _fireBaseConfig.Bucket,
                destinationObjectName,
                image.ContentType,
                image.OpenReadStream(),
                null,
                cancellationToken);

            imageModel.Path = await GetImagePublicUrl(destinationObjectName);

            return imageModel;
        }


        private static bool IsAllowedImageFormat(string imageFormat) => AllowedImageFormats.Contains(imageFormat);

        private async Task<string> GetImagePublicUrl(string destinationObjectName)
        {
            var storage = new FirebaseStorage(_fireBaseConfig.Bucket);

            var starsRef = storage.Child(destinationObjectName);

            return await starsRef.GetDownloadUrlAsync();
        }

        public async Task DeleteAsync(Image image, CancellationToken cancellationToken = default)
        {
            var storage = new FirebaseStorage(_fireBaseConfig.Bucket);
            await storage.Child($"{image.Id}.{image.Format}").DeleteAsync();
        }

      
    }
}
