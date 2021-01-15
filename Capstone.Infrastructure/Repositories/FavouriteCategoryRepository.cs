using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces.FavouriteCategoryInterfaces;
using Capstone.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Infrastructure.Repositories
{
    public class FavouriteCategoryRepository : BaseRepository<FavouriteCategory>, IFavouriteCategoryRepository
    {
        public FavouriteCategoryRepository(CapstoneContext context) : base(context) { }

        public IEnumerable<FavouriteCategory> GetFavouriteCategoryByUser(int? userId)
        {
            return _entities.Where(x => x.CustomerId == userId && x.IsDeleted == false).ToList();
        }

        public FavouriteCategory GetFavouriteCategoryForSuggest(int? userId)
        {
            return _entities.Where(x => x.CustomerId == userId && x.IsDeleted == false).OrderByDescending(x => x.Rating).FirstOrDefault();
        }

        public async Task AddFavouriteCategory(FavouriteCategoryDto favouriteCategory)
        {
            int?[] categoryId = favouriteCategory.CategoryId;
            for (int i = 0; i < categoryId.Length; i++)
            {
                var favourite = new FavouriteCategory
                {
                    CustomerId = favouriteCategory.CustomerId,
                    CategoryId = categoryId[i],
                    Rating = 1,
                    IsDeleted = false
                };
                await _entities.AddAsync(favourite);
            }          
        }
    }
}
