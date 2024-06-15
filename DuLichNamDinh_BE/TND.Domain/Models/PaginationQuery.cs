using System.Linq.Expressions;
using TND.Domain.Enums;

namespace TND.Domain.Models
{
    public record PaginationQuery<TEntity>(
        Expression<Func<TEntity, bool>> Filter,
        SortOrder SortOrder,
        string? SortColumn,
        int PageNumber,
        int PageSize) : Query<TEntity>(Filter, SortColumn, SortOrder);
}
