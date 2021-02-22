using Capstone.Core.CustomEntities;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Core.QueryFilters;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Core.Services
{
    public class BookCategoryService : IBookCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public BookCategoryService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public async Task<bool> DeleteBookCategory(int?[] id)
        {
            await _unitOfWork.BookCategoryRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public PagedList<BookCategory> GetBookCategories(BookCategoryQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var bookCategories = _unitOfWork.BookCategoryRepository.GetAll();           

            if (filters.CategoryId != null)
            {
                bookCategories = bookCategories.Where(x => x.CategoryId == filters.CategoryId);
            }

            var pagedBookCategories = PagedList<BookCategory>.Create(bookCategories, filters.PageNumber, filters.PageSize);
            return pagedBookCategories;
        }

        public async Task<BookCategory> GetBookCategory(int id)
        {
            return await _unitOfWork.BookCategoryRepository.GetById(id);
        }

        public async Task InsertBookCategory(BookCategory bookCategory)
        {
            await _unitOfWork.BookCategoryRepository.Add(bookCategory);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateBookCategory(BookCategory bookCategory)
        {
            _unitOfWork.BookCategoryRepository.Update(bookCategory);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
