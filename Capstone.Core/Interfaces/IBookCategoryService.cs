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
        Task<BookCategory> GetBookCategory(int id);
        Task InsertBookCategory(BookCategory bookCategory);
        Task<bool> UpdateBookCategory(BookCategory bookCategory);

        Task<bool> DeleteBookCategory(int id);
    }
}
