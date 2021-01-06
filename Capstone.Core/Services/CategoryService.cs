using Capstone.Core.CustomEntities;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Core.QueryFilters;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public CategoryService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public async Task<bool> DeleteCategory(int?[] id)
        {
            await _unitOfWork.CategoryRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public PagedList<Category> GetCategories(CategoryQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var categories = _unitOfWork.CategoryRepository.GetAll();
            if (filters.Name != null)
            {
                categories = categories.Where(x => x.Name == filters.Name);
            }

            var pagedCategories = PagedList<Category>.Create(categories, filters.PageNumber, filters.PageSize);
            return pagedCategories;
        }

        public async Task<Category> GetCategory(int id)
        {
            return await _unitOfWork.CategoryRepository.GetById(id);
        }

        public async Task InsertCategory(Category category)
        {
            await _unitOfWork.CategoryRepository.Add(category);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateCategory(Category category)
        {
            _unitOfWork.CategoryRepository.Update(category);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
