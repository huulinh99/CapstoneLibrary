using Capstone.Core.CustomEntities;
using Capstone.Core.Entities;
using Capstone.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces
{
    public interface IDrawerService
    {
        IEnumerable<Drawer> GetDrawers(DrawerQueryFilter filters);
        Task<Drawer> GetDrawer(int id);
        Task InsertDrawer(Drawer drawer);
        Task<bool> UpdateDrawer(Drawer drawer);
        Task<bool> DeleteDrawer(int?[] id);
    }
}
