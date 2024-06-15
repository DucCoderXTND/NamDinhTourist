using System.Linq.Expressions;
using TND.Domain.Enums;

namespace TND.Domain.Models
{
    public record Query<TEntity>(
        Expression<Func<TEntity, bool>> Filter,
        string? SortColumn,
        SortOrder SortOrder);
}
