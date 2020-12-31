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
    public class DrawerRepository : BaseRepository<Drawer>, IDrawerRepository
    {
        public DrawerRepository(CapstoneContext context) : base(context) { }
        //public async Task<IEnumerable<Drawer>> GetDrawersByBookShelf(int bookShelfId)
        //{
        //    return await _entities.Where(x => x.BookSheflId == bookShelfId).ToListAsync();
        //}
    }
}
