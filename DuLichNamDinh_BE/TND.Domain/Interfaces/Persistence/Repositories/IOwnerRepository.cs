using TND.Domain.Entities;
using TND.Domain.Models;

namespace TND.Domain.Interfaces.Persistence.Repositories
{
    public interface IOwnerRepository
    {
        Task<PaginatedList<Owner>> GetAsync(PaginationQuery<Owner> query,
            CancellationToken cancellationToken = default);
        Task<Owner?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Owner> CreateAsync(Owner owner, CancellationToken cancellationToken = default);
        Task UpdateAsync(Owner owner, CancellationToken cancellationToken = default);
    }
}
