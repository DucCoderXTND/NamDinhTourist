using TND.Domain.Entities;
using TND.Domain.Models;

namespace TND.Domain.Interfaces.Persistence.Repositories
{
    public interface IHotelRepository
    {
        Task<PaginatedList<HotelForManagement>> GetForManagementAsync(PaginationQuery<Hotel> query,
            CancellationToken cancellationToken = default);
        Task<Hotel?> GetByIdAsync(Guid id, bool includeCity = false, 
            bool includeThumbnail = false, bool includeGallery = false,
            CancellationToken cancellationToken = default);
        Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Hotel> CreateAsync(Hotel hotel, CancellationToken cancellationToken = default);
        Task UpdateAsync(Hotel hotel, CancellationToken cancelToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> ExistsByCityIdAsync(Guid cityId,  CancellationToken cancellationToken = default);
        Task<bool> ExistsByLocation(double longitude, double latitude);
        Task<PaginatedList<HotelSearchResult>> GetForSearchAsync(PaginationQuery<Hotel> query,
            CancellationToken cancellationToken = default);
        Task UpdateReviewById(Guid id, double newRating, CancellationToken cancellationToken = default);
        
    }
}
