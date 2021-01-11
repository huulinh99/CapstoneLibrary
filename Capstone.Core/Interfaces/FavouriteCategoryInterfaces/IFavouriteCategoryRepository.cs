using Capstone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces.FavouriteCategoryInterfaces
{
    public interface IFavouriteCategoryRepository : IRepository<FavouriteCategory>
    {
        IEnumerable<FavouriteCategory> GetFavouriteCategoryByUser(int? userId);
        FavouriteCategory GetFavouriteCategoryForSuggest(int? userId);
    }
}
