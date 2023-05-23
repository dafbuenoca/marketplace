using System.Collections.Generic;
using System;

namespace Marketplace.Api.Controllers
{
    public class PagedList<T>
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int PageCount => (int)Math.Ceiling((double)TotalCount / PageSize);

        public PagedList(List<T> items, int totalCount, int pageIndex, int pageSize )
        {
            Items = items;
            TotalCount = totalCount;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }
}
