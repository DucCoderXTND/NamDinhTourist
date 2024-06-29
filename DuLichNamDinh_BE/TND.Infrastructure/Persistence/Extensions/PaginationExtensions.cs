using LinqToDB;
using TND.Domain.Models;

namespace TND.Infrastructure.Persistence.Extensions
{
    public static class PaginationExtensions
    {
        public static IQueryable<TItem> GetPage<TItem>(this IQueryable<TItem> queryable,
            int pageNumber, int pageSize)
        {
            return queryable.Skip(pageSize * (pageNumber - 1))
                .Take(pageSize);
        }
        public static async Task<PaginationMetadata> GetPaginationMetadataAsync<TItem>(
            this IQueryable<TItem> queryable, int pageNumber, int pageSize)
        {
            return new PaginationMetadata(await queryable.CountAsync(),
                pageNumber, pageSize);
        }
    }
}
