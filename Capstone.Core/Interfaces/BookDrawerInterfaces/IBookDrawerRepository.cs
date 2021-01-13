using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces.BookDrawerInterfaces
{
    public interface IBookDrawerRepository : IRepository<BookDrawer>
    {
        IEnumerable<BookDrawer> GetBookDrawerByDrawerId(int? drawerId);
        Task DeleteBookDrawerByDrawerId(int?[] drawerId);
        Task GetBookDrawerByListBookId(int?[] bookId);
        BookDrawer GetBookDrawerByBookId(int? bookId);
        IEnumerable<BookDrawer> GetBookDrawerByListBook(IEnumerable<BookDto> books);
    }
}
