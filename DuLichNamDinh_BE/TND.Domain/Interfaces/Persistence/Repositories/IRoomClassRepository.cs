using TND.Domain.Entities;
using TND.Domain.Models;

namespace TND.Domain.Interfaces.Persistence.Repositories
{
    public interface IRoomClassRepository
    {
        Task<RoomClass?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<RoomClass> CreateAsync(RoomClass roomClass, CancellationToken cancellationToken = default);
        Task UpdateAsync(RoomClass roomClass, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> ExistsByHotelIdAsync(Guid hotelId,  CancellationToken cancellationToken = default);
        Task<PaginatedList<RoomClass>> GetAsync(
            PaginationQuery<RoomClass> query, 
            bool includeGallery = false,
            CancellationToken cancellationToken = default);
        Task<bool> ExistsByNameInHotelAsync(Guid hotelId,
            string name, CancellationToken cancellationToken = default);
        Task<IEnumerable<RoomClass>> GetFeatureDealsInDifferentHotelsAsync(
            int count, CancellationToken cancellationToken = default);
    }
}
