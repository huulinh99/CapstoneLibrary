using Capstone.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capstone.Core.CustomEntities
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public Uri NextPage { get; set; }
        public Uri PreviousPage { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
        public int? NextPageNumber => HasNextPage ? CurrentPage + 1 : (int?)null;
        public int? PreviousPageNumber => HasPreviousPage ? CurrentPage - 1 : (int?)null;

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            AddRange(items);
        }

        public static PagedList<T> Create(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }

        //public static PagedList<T> Create(IEnumerable<T> source, int pageSize, int pageNumber, UriService uriService)
        //{
        //    var count = source.Count();
        //    var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        //    var respose = new PagedList<T>(items, count, pageSize, pageNumber);
        //    var totalPages = ((double)count / (double)pageSize);
        //    //int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
        //    respose.NextPage =
        //        pageNumber >= 1 && pageNumber < roundedTotalPages
        //        ? uriService.GetPageUri(pageNumber + 1, pageSize)
        //        : null;
        //    respose.PreviousPage =
        //        pageNumber - 1 >= 1 && pageNumber <= roundedTotalPages
        //        ? uriService.GetPageUri(pageNumber - 1, pageSize)
        //        : null;
        //    return respose;
        //}

    }
}
