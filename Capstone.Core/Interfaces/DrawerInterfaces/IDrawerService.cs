using Capstone.Core.CustomEntities;
using Capstone.Core.DTOs;
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
        IEnumerable<DrawerDto> GetDrawers(DrawerQueryFilter filters);
        Drawer GetDrawer(int id);
        void InsertDrawer(Drawer drawer);
        bool UpdateDrawer(Drawer drawer);
        bool DeleteDrawer(int?[] id);
    }
}
