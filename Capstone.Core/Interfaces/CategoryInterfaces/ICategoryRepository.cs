using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<CategoryDto> GetCategoriesByName(string name);

        ICollection<CategoryDto> GetCategoryNameByBookCategory(IEnumerable<BookCategory> bookCategory);
        ICollection<CategoryDto> GetAllCategories();
    }
}
