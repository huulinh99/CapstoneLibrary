using Capstone.Core.DTOs;
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
        IEnumerable<BookDto> GetAllBooks();
        IEnumerable<BookDto> GetAllBooksInDrawer();
        void DeleteBookByBookDrawerId(int?[] bookDrawerId);
        IEnumerable<BookDto> GetAllBooksNotInDrawer();
        IEnumerable<BookDto> GetBookInDrawer(IEnumerable<BookDrawer> bookDrawers);
        IEnumerable<BookDto> GetBookByBookGroup(int? bookGroupId);
        void GetBookByBookDrawerId(int?[] bookDrawerId);
        List<IEnumerable<BookDto>> GetBookByBookGroupWithDrawer(IEnumerable<BookGroupDto> bookGroups, IEnumerable<DrawerDto> drawers);
    }
}
