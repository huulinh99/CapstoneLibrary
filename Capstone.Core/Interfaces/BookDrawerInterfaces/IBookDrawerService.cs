using Capstone.Core.CustomEntities;
using Capstone.Core.Entities;
using Capstone.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces.BookDrawerInterfaces
{
    public interface IBookDrawerService
    {
        PagedList<BookDrawer> GetBookDrawers(BookDrawerQueryFilter filters);
        Task<BookDrawer> GetBookDrawer(int id);
        Task InsertBookDrawer(BookDrawer bookDrawer);
        Task<bool> UpdateBookDrawer(BookDrawer bookDrawer);
        Task<bool> DeleteBookDrawer(int?[] id);
    }
}
