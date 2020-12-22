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
        Task<Location> GetLocation(int id);
        Task InsertLocation(Location location);
        Task<bool> UpdateLocation(Location location);

        Task<bool> DeleteLocation(int id);
    }
}
