using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Models.Paging;
using System.Linq.Expressions;

namespace RealEstate.Application.Helpers
{
    public static class Grid
    {
        /// <summary>
        /// Sorts elements in the given order and reduces its size 
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="source">Source list</param>
        /// <param name="gridModel">Sorting/Pagination parameters</param>
        /// <returns></returns>
        public static async Task<GridBaseResponseModel<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, GridBaseRequestModel gridModel, bool sorting = true)
        {
            if (gridModel == null)
            {
                gridModel = new GridBaseRequestModel();
            }

            if (sorting)
            {
                var prop = typeof(T).GetProperty(gridModel.Sorting?.SortBy);

                if (prop != null)
                {
                    var isAscending = gridModel.Sorting?.SortDir == "asc";
                    source = source.OrderByAsQueryable(prop.Name, isAscending);
                }
            }
            var result = new GridBaseResponseModel<T>();
            result.Items = await source.Skip(gridModel.Paging.Offset).Take(gridModel.Paging.Limit).ToListAsync();
            result.TotalCount = await source.CountAsync();

            //result.CurrentElementStart = gridModel?.Paging?.Offset ?? 0;
            //result.CurrentElementEnd = (gridModel?.Paging?.Offset ?? 0) + (gridModel?.Paging?.Limit ?? 0);

            return result;
        }



        public static IQueryable<T> OrderByAsQueryable<T>(this IQueryable<T> source, string columnName, bool isAscending = true)
        {
            if (string.IsNullOrEmpty(columnName))
            {
                return source;
            }

            //ParameterExpression parameter = Expression.Parameter(source.ElementType, "");

            //MemberExpression property = Expression.Property(parameter, columnName);
            //LambdaExpression lambda = Expression.Lambda(property, parameter);

            //string methodName = isAscending ? "OrderBy" : "OrderByDescending";

            //Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName,
            //    new Type[] { source.ElementType, property.Type },
            //    source.Expression, Expression.Quote(lambda));

            //return source.Provider.CreateQuery<T>(methodCallExpression);

            //====================================

            string command = isAscending ? "OrderBy" : "OrderByDescending";
            var type = typeof(T);
            var property = type.GetProperty(columnName);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType },
                source.Expression, Expression.Quote(orderByExpression));

            return source.Provider.CreateQuery<T>(resultExpression);
        }
    }

    public static class AsyncEnumerableExtensions
    {
        public static async IAsyncEnumerable<T> ToAsyncEnumerable<T>(this IEnumerable<T> enumerable)
        {
            using IEnumerator<T> enumerator = enumerable.GetEnumerator();

            while (await Task.Run(enumerator.MoveNext).ConfigureAwait(false))
            {
                yield return enumerator.Current;
            }
        }
    }
}
