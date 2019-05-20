using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace OnlineShop.Application.Pagination
{
    public class PagedResult
    {
        public int TotalCount { get; set; }
        public int TotalPages => (TotalCount / PageSize) + TotalCount % PageSize == 0 ? 0 : 1;
        public int PageSize { get; set; }
        public int Page { get; set; }
        public IList<object> Result { get; set; }

        public static PagedResult FromAnonymous<TEntity>(IQueryable<TEntity> query, PageDto pageDto)
        {
            var pagedResult = new PagedResult<TEntity>();

            //apply filtering
            query = FilterFlat(query, pageDto);

            //calc total count
            pagedResult.TotalCount = query.Count();

            //apply sorting
            query = Sort(query, pageDto);

            //apply paging
            query = Paginate(query, pageDto);

            //materialize
            pagedResult.Result = query.ToList();

            pagedResult.Page = pageDto.Page;

            pagedResult.PageSize = pageDto.PageSize;

            return pagedResult;
        }

        protected static IQueryable<TEntity> FilterFlat<TEntity>(IQueryable<TEntity> query, PageDto pageDto)
        {
            if (!string.IsNullOrWhiteSpace(pageDto.Filter) && pageDto.FilterFields.Any())
            {
                var whereExpr = ExpressionBuilder.WhereExpression<TEntity>(pageDto.FilterFields.Split(','), pageDto.Filter);
                query = query.Where(whereExpr);
            }
            return query;
        }


        protected static IQueryable<TEntity> Paginate<TEntity>(IQueryable<TEntity> query, PageDto pageDto)
        {
            return query.Skip((pageDto.Page - 1) * pageDto.PageSize).Take(pageDto.PageSize);
        }

        protected static IQueryable<TEntity> Sort<TEntity>(IQueryable<TEntity> query, PageDto pageDto)
        {
            if (!string.IsNullOrWhiteSpace(pageDto.Sort))
            {
                var memberExpr = ExpressionBuilder.MemberExpression<TEntity>(pageDto.Sort);
                query = pageDto.Ascending ? query.OrderBy(memberExpr) : query.OrderByDescending(memberExpr);
            }
            return query;
        }
    }
    public class PagedResult<TViewModel> : PagedResult
    {
        public new IList<TViewModel> Result { get; set; }

        public static PagedResult<TViewModel> From<TEntity>(
            IQueryable<TEntity> query,
            PageDto pageDto,
            IConfigurationProvider mapConfig,
            Func<IQueryable<TEntity>, IQueryable<TViewModel>> map = null
            ) where TEntity : class
        {
            var pagedResult = new PagedResult<TViewModel>();

            //apply filtering
            query = FilterFlat(query, pageDto);

            pagedResult.TotalCount = query.Count();

            //apply sorting
            query = Sort(query, pageDto);

            //apply projection
            IQueryable<TViewModel> projectedResult = map is null ? Extensions.ProjectTo<TViewModel>(query, mapConfig) : map(query);

            //paginate and materialize
            pagedResult.Result = Paginate(projectedResult, pageDto).ToList();

            pagedResult.Page = pageDto.Page;
            pagedResult.PageSize = pageDto.PageSize;

            return pagedResult;
        }
    }
}
