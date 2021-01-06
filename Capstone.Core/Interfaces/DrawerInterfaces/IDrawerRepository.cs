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
        IEnumerable<Drawer> GetAllDrawers();
        Task DeleteDrawerInBookShelf(int?[] bookShelfId);
    }   
}
