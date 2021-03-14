using Capstone.Core.CustomEntities;
using Capstone.Core.Entities;
using Capstone.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces
{
    public interface ILocationService 
    {
        PagedList<Location> GetLocations(LocationQueryFilter filters);
        Location GetLocation(int id);
        void InsertLocation(Location location);
        bool UpdateLocation(Location location);

        bool DeleteLocation(int?[] id);
    }
}
