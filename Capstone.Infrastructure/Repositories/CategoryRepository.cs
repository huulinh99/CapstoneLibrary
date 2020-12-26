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
        public CategoryRepository(CapstoneContext context) : base(context) { }
        public async Task<IEnumerable<Category>> GetCategoriesByName(string name)
        {
            return await _entities.Where(x => x.Name == name).ToListAsync();
        }
    }
}
