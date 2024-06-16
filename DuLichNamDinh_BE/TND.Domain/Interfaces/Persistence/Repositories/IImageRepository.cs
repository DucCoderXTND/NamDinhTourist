using Microsoft.AspNetCore.Http;
using TND.Domain.Entities;
using TND.Domain.Enums;

namespace TND.Domain.Interfaces.Persistence.Repositories
{
    public interface IImageRepository
    {
        Task<Image> CreateAsync(IFormFile image, Guid entityId, ImageType type,
            CancellationToken cancellationToken = default);
        Task DeleteForAsync(Guid entityId, ImageType type,
            CancellationToken cancellationToken = default);
    }
}
