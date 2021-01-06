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
        Task<BookDrawer> GetBookDrawerByBookId(int? bookId);
    }
}
