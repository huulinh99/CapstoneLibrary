using Capstone.Core.CustomEntities;
using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces.FavouriteCategoryInterfaces
{
    public interface IFavouriteCategoryService
    {
        PagedList<FavouriteCategory> GetFavouriteCategories(FavouriteCategoryQueryFilter filters);
        FavouriteCategory GetFavouriteCategory(int id);
        void InsertFavouriteCategory(FavouriteCategoryDto favouriteCategory);
        bool UpdateFavouriteCategory(FavouriteCategory favouriteCategory);
        bool DeleteFavouriteCategory(int?[] id);
    }
}
