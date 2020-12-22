﻿using Capstone.Core.Entities;
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
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(CapstoneContext context) : base(context) { }
        public async Task<IEnumerable<Book>> GetBooksByBookGroup(int bookGroupId)
        {
            return await _entities.Where(x => x.BookGroupId == bookGroupId).ToListAsync();
        }
    }
}