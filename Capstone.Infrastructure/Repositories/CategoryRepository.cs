using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(CapstoneContext context) : base(context) {
        }

        public  ICollection<CategoryDto> GetAllCategories()
        {
            return _entities.Include(x => x.BookCategory).Select(x => new CategoryDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesByName(string name)
        {
            return await _entities.Where(x => x.Name == name).Select(x => new CategoryDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
        }


        public ICollection<CategoryDto> GetCategoryNameByBookCategory(IEnumerable<BookCategory> bookCategories)
        {
            List<CategoryDto> categories = new List<CategoryDto>();
            foreach (var bookCategory in bookCategories)
            {
                var category = _entities.Where(x => x.Id == bookCategory.CategoryId).Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name
                }).FirstOrDefault();
                categories.Add(category);
            }
            return categories;
        }
    }
}
