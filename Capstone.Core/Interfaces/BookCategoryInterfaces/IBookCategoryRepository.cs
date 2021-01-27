using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces
{
    public interface IBookCategoryRepository : IRepository<BookCategory>
    {
        IEnumerable<BookCategory> GetBookCategoriesByCategory(int? categoryId);
        IEnumerable<BookCategory> GetBookCategoriesByBookGroup(int? bookGroupId);
        IEnumerable<BookCategory> GetAllBookCategoriesByBookGroup();
    }
}
