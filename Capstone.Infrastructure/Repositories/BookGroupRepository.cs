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
    public class BookGroupRepository : BaseRepository<BookGroup>, IBookGroupRepository
    {
        public BookGroupRepository(CapstoneContext context) : base(context) { }

        public async Task<IEnumerable<BookGroup>> GetBookGroupsByName(string bookGroupName)
        {
            return await _entities.Where(x => x.Name.Contains(bookGroupName)).ToListAsync();
        }
    }
}
