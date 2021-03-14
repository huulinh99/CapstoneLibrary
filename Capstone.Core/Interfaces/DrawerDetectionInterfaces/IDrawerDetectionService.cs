using Capstone.Core.CustomEntities;
using Capstone.Core.Entities;
using Capstone.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.Interfaces.DrawerDetectionInterfaces
{
    public interface IDrawerDetectionService
    {
        PagedList<DrawerDetection> GetDrawerDetections(DrawerDetectionQueryFilter filters);
        DrawerDetection GetDrawerDetection(int id);
        void InsertDrawerDetection(DrawerDetection drawerDetection);
        bool UpdateDrawerDetection(DrawerDetection drawerDetection);
        bool DeleteDrawerDetection(int?[] id);
    }
}
