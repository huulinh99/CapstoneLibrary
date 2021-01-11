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
        Task<FavouriteCategory> GetFavouriteCategory(int id);
        Task InsertFavouriteCategory(FavouriteCategory favouriteCategory);
        Task<bool> UpdateFavouriteCategory(FavouriteCategory favouriteCategory);
        Task<bool> DeleteFavouriteCategory(int?[] id);
    }
}
