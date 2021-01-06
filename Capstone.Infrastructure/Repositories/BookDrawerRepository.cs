using Capstone.Core.Entities;
using Capstone.Core.Interfaces.BookDrawerInterfaces;
using Capstone.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Infrastructure.Repositories
{
    public class BookDrawerRepository : BaseRepository<BookDrawer>, IBookDrawerRepository
    {
        public BookDrawerRepository(CapstoneContext context) : base(context) { }

        public IEnumerable<BookDrawer> GetBookDrawerByDrawerId(int? drawerId)
        {
            return _entities.Where(x => x.DrawerId == drawerId && x.IsDeleted == false).ToList();
        }

        public Task<BookDrawer> GetBookDrawerByBookId(int? bookId)
        {
            return _entities.Where(x => x.BookId == bookId && x.IsDeleted == false).FirstOrDefaultAsync();
        }
    }
}
