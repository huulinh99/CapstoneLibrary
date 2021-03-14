using Capstone.Core.CustomEntities;
using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Core.Interfaces.FavouriteCategoryInterfaces;
using Capstone.Core.QueryFilters;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public bool DeleteFavouriteCategory(int?[] id)
        {
            _unitOfWork.FavouriteCategoryRepository.Delete(id);
            _unitOfWork.SaveChanges();
            return true;
        }

        public PagedList<FavouriteCategory> GetFavouriteCategories(FavouriteCategoryQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var favouriteCategories = _unitOfWork.FavouriteCategoryRepository.GetAll();


            if (filters.CustomerId != null)
            {
                favouriteCategories = favouriteCategories.Where(x => x.CustomerId == filters.CustomerId);
            }
            var pagedFavouriteCategories = PagedList<FavouriteCategory>.Create(favouriteCategories, filters.PageNumber, filters.PageSize);
            return pagedFavouriteCategories;
        }

        public FavouriteCategory GetFavouriteCategory(int id)
        {
            return _unitOfWork.FavouriteCategoryRepository.GetById(id);
        }


        public void InsertFavouriteCategory(FavouriteCategoryDto favouriteCategory)
        {
            _unitOfWork.FavouriteCategoryRepository.AddFavouriteCategory(favouriteCategory);
            _unitOfWork.SaveChanges();
        }

        public bool UpdateFavouriteCategory(FavouriteCategory favouriteCategory)
        {
            _unitOfWork.FavouriteCategoryRepository.Update(favouriteCategory);
            _unitOfWork.SaveChanges();
            return true;
        }

    }
}
