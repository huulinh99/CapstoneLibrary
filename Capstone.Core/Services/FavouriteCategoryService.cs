using Capstone.Core.CustomEntities;
using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Core.Interfaces.FavouriteCategoryInterfaces;
using Capstone.Core.QueryFilters;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Services
{
    public class FavouriteCategoryService : IFavouriteCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public FavouriteCategoryService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public Task<bool> DeleteFavouriteCategory(int?[] id)
        {
            throw new NotImplementedException();
        }

        public Task<FavouriteCategory> GetDrawer(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FavouriteCategoryDto> GetDrawers(FavouriteCategoryQueryFilter filters)
        {
            throw new NotImplementedException();
        }

        public Task InsertFavouriteCategory(FavouriteCategory favouriteCategory)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateFavouriteCategory(FavouriteCategory favouriteCategory)
        {
            throw new NotImplementedException();
        }
    }
}
