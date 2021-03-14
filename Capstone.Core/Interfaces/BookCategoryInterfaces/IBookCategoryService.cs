using Capstone.Core.CustomEntities;
using Capstone.Core.Entities;
using Capstone.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces
{
    public interface IBookCategoryService
    {
        PagedList<BookCategory> GetBookCategories(BookCategoryQueryFilter filters);
        BookCategory GetBookCategory(int id);
        void InsertBookCategory(BookCategory bookCategory);
        bool UpdateBookCategory(BookCategory bookCategory);
        bool DeleteBookCategory(int?[] id);
    }
}
