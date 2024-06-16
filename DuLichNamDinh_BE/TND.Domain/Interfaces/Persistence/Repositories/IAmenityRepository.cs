using TND.Domain.Entities;
using TND.Domain.Models;

namespace TND.Domain.Interfaces.Persistence.Repositories
{
    public interface IAmenityRepository
    {
        Task<PaginatedList<Amenity>> GetAsync(PaginationQuery<Amenity> query,
            CancellationToken cancellationToken = default);
        Task<Amenity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<Amenity> CreateAsync(Amenity amenity, CancellationToken cancellationToken = default);
        Task UpdateAsync(Amenity amenity, CancellationToken cancellationToken = default);
    }
}
