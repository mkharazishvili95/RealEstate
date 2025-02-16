using RealEstate.Common.Models;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace RealEstate.Common.Extensions
{
    public static class IEnumerableExtensions
    {
        public static bool AnyAndNotNull<TSource>([AllowNull] this IEnumerable<TSource> source)
        {
            if (source == null)
                return false;
            return source.Any();
        }

        public static string SelectToCommaSeparatedString<TSource>([AllowNull] this IEnumerable<TSource> source) where TSource : IConvertible
        {
            return source is null ? string.Empty : $"{string.Join(",", source.Select(x => Convert.ToInt32(x)))}";
        }

        public static IQueryable<TSource> WhereIsNotDeleted<TSource>
            (this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate) where TSource : AuditableEntity
        {
            return source.Where(x => !x.IsDeleted).Where(predicate);
        }
    }
}
