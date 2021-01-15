using Capstone.Core.DTOs;
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
        Task AddFavouriteCategory(FavouriteCategoryDto favouriteCategory);
        FavouriteCategory GetFavouriteCategoryForSuggest(int? userId);
    }
}
