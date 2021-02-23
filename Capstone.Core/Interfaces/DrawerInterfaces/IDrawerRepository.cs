using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces
{
    public interface IDrawerRepository : IRepository<Drawer>
    {
        //Task<IEnumerable<Drawer>> GetDrawersByBookShelf(int bookShelfId);
        IEnumerable<DrawerDto> GetAllDrawers(int bookShelfId, int rowStart, int rowEnd, int colStart, int colEnd);
        void DeleteDrawerInBookShelf(int?[] bookShelfId);
        IEnumerable<DrawerDto> GetAllDrawers(IEnumerable<BookDrawer> bookDrawers);
        int?[] GetDrawerIdInBookShelf(int?[] bookShelfId);
    }   
}
