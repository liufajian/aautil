using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace System.Linq
{
    public static class QueryExtensions
    {
        private static IOrderedQueryable<T> OrderingHelper<T>(IQueryable<T> source, string propertyName, bool descending, bool first)
        {
            var param = Expression.Parameter(typeof(T), string.Empty); // I don't care about some naming
            var property = Expression.PropertyOrField(param, propertyName);
            var sort = Expression.Lambda(property, param);
            var call = Expression.Call(
                typeof(Queryable),
                (first ? "OrderBy" : "ThenBy") + (descending ? "Descending" : string.Empty),
                new[] { typeof(T), property.Type },
                source.Expression,
                Expression.Quote(sort));
            return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(call);
        }

        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
        {
            return OrderingHelper(source, propertyName, false, true);
        }

        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName)
        {
            return OrderingHelper(source, propertyName, true, true);
        }

        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string propertyName)
        {
            return OrderingHelper(source, propertyName, false, false);
        }

        public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, string propertyName)
        {
            return OrderingHelper(source, propertyName, true, false);
        }

        /// <summary>
        /// 根据排序表达式(field direction,...)进行查询排序
        /// </summary>
        public static IQueryable<T> SortBy<T>(this IQueryable<T> query, string sortExpression)
        {
            if (query == null)
            {
                throw new ArgumentNullException("source");
            }

            if (string.IsNullOrWhiteSpace(sortExpression))
            {
                return query;
            }

            string sortDirection;
            string propertyName;

            var sorts = sortExpression.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            for (var i = 0; i < sorts.Length; i++)
            {
                sortExpression = sorts[i].Trim();
                var spaceIndex = sortExpression.IndexOf(" ");
                if (spaceIndex < 0)
                {
                    propertyName = sortExpression;
                    sortDirection = "ASC";
                }
                else
                {
                    propertyName = sortExpression.Substring(0, spaceIndex);
                    sortDirection = sortExpression.Substring(spaceIndex + 1).Trim();
                }

                if (string.IsNullOrEmpty(propertyName))
                {
                    return query;
                }

                var parameter = Expression.Parameter(query.ElementType, string.Empty);
                var property = Expression.Property(parameter, propertyName);
                var lambda = Expression.Lambda(property, parameter);

                var methodName = (i == 0 ? "OrderBy" : "ThenBy") +
                    (sortDirection.Equals("ASC", StringComparison.OrdinalIgnoreCase) ? string.Empty : "Descending");
                Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                                                    new[] { query.ElementType, property.Type },
                                                    query.Expression, Expression.Quote(lambda));
                query = query.Provider.CreateQuery<T>(methodCallExpression);
            }

            return query;
        }

        /// <summary>
        /// 适用于单个字段排序的查询
        /// </summary>
        public static IQueryable<T> SortBy<T>(this IQueryable<T> query, string sortField, string sortOrder)
        {
            if (query == null)
            {
                throw new ArgumentNullException("source");
            }

            if (string.IsNullOrWhiteSpace(sortField))
            {
                return query;
            }

            sortOrder = (sortOrder ?? string.Empty).Trim();

            var parameter = Expression.Parameter(query.ElementType, string.Empty);
            var property = Expression.Property(parameter, sortField);
            var lambda = Expression.Lambda(property, parameter);

            var methodName = "OrderBy" + (sortOrder.Equals("DESC", StringComparison.OrdinalIgnoreCase) ? "Descending" : string.Empty);

            Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                                                new[] { query.ElementType, property.Type },
                                                query.Expression, Expression.Quote(lambda));

            query = query.Provider.CreateQuery<T>(methodCallExpression);

            return query;
        }

        /// <summary>
        /// 适用于多个字段单方向排序的查询
        /// </summary>
        public static IQueryable<T> SortBy<T>(this IQueryable<T> query, IEnumerable<string> sortFields, string sortOrder)
        {
            if (query == null)
            {
                throw new ArgumentNullException("source");
            }

            if (sortFields == null || !sortFields.Any())
            {
                return query;
            }

            sortOrder = (sortOrder ?? string.Empty).Trim();

            int i = -1;

            foreach (var s in sortFields)
            {
                if (string.IsNullOrEmpty(s))
                {
                    return query;
                }

                i++;

                var parameter = Expression.Parameter(query.ElementType, string.Empty);
                var property = Expression.Property(parameter, s);
                var lambda = Expression.Lambda(property, parameter);

                var methodName = (i == 0 ? "OrderBy" : "ThenBy") +
                    (sortOrder.Equals("ASC", StringComparison.OrdinalIgnoreCase) ? string.Empty : "Descending");
                Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                                                    new[] { query.ElementType, property.Type },
                                                    query.Expression, Expression.Quote(lambda));
                query = query.Provider.CreateQuery<T>(methodCallExpression);
            }

            return query;
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        public static IPagedList<T> ToPagedList<T>(this IQueryable<T> query, PagedQueryModel pageQuery)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            if (pageQuery == null)
            {
                throw new ArgumentNullException(nameof(pageQuery));
            }

            if (query.Expression.Type != typeof(IOrderedQueryable<T>))
            {
                if (!string.IsNullOrEmpty(pageQuery.SortExpression))
                {
                    query = query.SortBy(pageQuery.SortExpression);
                }
                else if (typeof(T).GetProperty("id", Reflection.BindingFlags.Instance | Reflection.BindingFlags.Public | Reflection.BindingFlags.IgnoreCase) != null)
                {
                    query = query.SortBy("id", "DESC");
                }
            }

            return ToPagedList(query, pageQuery.GetTotal, pageQuery.PageIndex, pageQuery.PageSize);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="query">查询对象(必须有排序)</param>
        /// <param name="getTotal">是否获取总数</param>
        /// <param name="pageIndex">分页索引，从1开始</param>
        /// <param name="pageSize">分页显示数</param>
        /// <returns></returns>
        public static IPagedList<T> ToPagedList<T>(this IQueryable<T> query, bool getTotal, int pageIndex, int pageSize = PagedQueryModel.DefaultPageSize)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            if (pageSize == 0)
            {
                pageSize = 100000;
            }

            pageIndex = pageIndex > 0 ? pageIndex : 1;
            pageSize = pageSize > 0 ? pageSize : PagedQueryModel.DefaultPageSize;

            var pagedList = new PagedList<T>
            {
                Items = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList(),
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            if (getTotal)
            {
                pagedList.TotalCount = query.Count();
            }

            return pagedList;
        }

        /// <summary>
        /// Converts the specified source to <see cref="IPagedList{T}"/> by the specified <paramref name="pageIndex"/> and <paramref name="pageSize"/>.
        /// </summary>
        /// <typeparam name="T">The type of the source.</typeparam>
        /// <param name="query">The source to paging.</param>
        /// <param name="pageIndex">The index of the page.</param>
        /// <param name="pageSize">The size of the page.</param>
        /// <returns>An instance of the inherited from <see cref="IPagedList{T}"/> interface.</returns>
        public static IPagedList<T> ToPagedList<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        {
            var count = query.Count();
            var items = query.Skip((pageIndex) * pageSize).Take(pageSize).ToList();
            var pagedList = new PagedList<T>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = count,
                Items = items
            };
            return pagedList;
        }

        /// <summary>
        /// Converts the specified source to <see cref="IPagedList{T}"/> by the specified <paramref name="pageIndex"/> and <paramref name="pageSize"/>.
        /// </summary>
        /// <typeparam name="T">The type of the source.</typeparam>
        /// <param name="source">The source to paging.</param>
        /// <param name="pageIndex">The index of the page.</param>
        /// <param name="pageSize">The size of the page.</param>
        /// <param name="indexFrom">The start index value.</param>
        /// <returns>An instance of the inherited from <see cref="IPagedList{T}"/> interface.</returns>
        public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (source is IQueryable<T>)
            {
                source = source.AsEnumerable();
            }

            return new PagedList<T>(source, pageIndex, pageSize);
        }

        /// <summary>
        /// Converts the specified source to <see cref="IPagedList{T}"/> by the specified <paramref name="converter"/>, <paramref name="pageIndex"/> and <paramref name="pageSize"/>
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TResult">The type of the result</typeparam>
        /// <param name="source">The source to convert.</param>
        /// <param name="converter">The converter to change the <typeparamref name="TSource"/> to <typeparamref name="TResult"/>.</param>
        /// <param name="pageIndex">The page index.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="indexFrom">The start index value.</param>
        /// <returns>An instance of the inherited from <see cref="IPagedList{T}"/> interface.</returns>
        public static IPagedList<TResult> ToPagedList<TSource, TResult>(this IEnumerable<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter, int pageIndex, int pageSize)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (source is IQueryable<TSource>)
            {
                source = source.AsEnumerable();
            }

            return new PagedList<TSource, TResult>(source, converter, pageIndex, pageSize);
        }
    }
}
