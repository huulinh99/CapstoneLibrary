using Capstone.Core.CustomEntities;
using Capstone.Core.Entities;
using Capstone.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces
{
    public interface IBookRecommendService
    {
        PagedList<BookRecommend> GetBookRecommend(BookRecommendQueryFilter filters);
        BookRecommend GetBookRecommend(int id);
        void InsertBookRecommend(BookRecommend bookRecommend);
        bool UpdateBookRecommend(BookRecommend bookRecommend);
        bool DeleteBookRecommend(int?[] id);
    }
}
