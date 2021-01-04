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
    public class BookCategoryRepository : BaseRepository<BookCategory>, IBookCategoryRepository
    {
        public BookCategoryRepository(CapstoneContext context) : base(context) { }      

        public async Task<IEnumerable<BookCategory>> GetBookCategoriesByCategory(int? categoryId)
        {
            return await _entities.Where(x => x.CategoryId == categoryId && x.IsDeleted == false).ToListAsync();
        }

        public async Task<IEnumerable<BookCategory>> GetBookCategoriesByBookGroup(int? bookGroupId)
        {
            return await _entities.Where(x => x.BookGroupId == bookGroupId && x.IsDeleted == false).ToListAsync();
        }

    }
}
