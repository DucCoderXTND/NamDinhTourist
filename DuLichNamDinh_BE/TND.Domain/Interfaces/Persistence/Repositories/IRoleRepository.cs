using TND.Domain.Entities;

namespace TND.Domain.Interfaces.Persistence.Repositories
{
    public interface IRoleRepository
    {
        Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}
