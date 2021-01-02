using Capstone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces
{
    public interface IBookCategoryRepository : IRepository<BookCategory>
    {
        Task<IEnumerable<BookCategory>> GetBookCategoriesByCategory(int? categoryId);
        Task<IEnumerable<BookCategory>> GetBookCategoriesByBookGroup(int? bookGroupId);
    }
}
