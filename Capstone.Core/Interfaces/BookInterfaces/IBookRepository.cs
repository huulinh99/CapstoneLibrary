﻿using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        // Task<IEnumerable<Book>> GetBooksByBookGroup(int bookGroupId);
        IEnumerable<BookDto> GetAllBooksNotInDrawer();
        IEnumerable<BookDto> GetAllBooksInDrawer();
        IEnumerable<BookDto> GetBookInDrawer(IEnumerable<BookDrawer> bookDrawers);
    }
}
