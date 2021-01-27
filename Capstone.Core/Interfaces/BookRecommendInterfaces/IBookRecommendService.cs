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
        Task<BookRecommend> GetBookRecommend(int id);
        Task InsertBookRecommend(BookRecommend bookRecommend);
        Task<bool> UpdateBookRecommend(BookRecommend bookRecommend);
        Task<bool> DeleteBookRecommend(int?[] id);
    }
}
