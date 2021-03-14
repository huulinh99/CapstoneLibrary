using Capstone.Core.CustomEntities;
using Capstone.Core.Entities;
using Capstone.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces
{
    public interface ICategoryService
    {
        PagedList<Category> GetCategories(CategoryQueryFilter filters);
        Category GetCategory(int id);
        void InsertCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(int?[] id);
    }
}
