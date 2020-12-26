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
    public class BookShelfRepository : BaseRepository<BookShelf>, IBookShelfRepository
    {
        public BookShelfRepository(CapstoneContext context) : base(context) { }
        public async Task<IEnumerable<BookShelf>> GetBookShelvesByLocation(int locationId)
        {
            return await _entities.Where(x => x.LocationId == locationId).ToListAsync();
        }
    }
}
