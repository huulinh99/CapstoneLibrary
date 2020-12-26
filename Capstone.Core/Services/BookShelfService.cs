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
    public class BookShelfService : IBookShelfService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public BookShelfService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public async Task<bool> DeleteBookShelf(int id)
        {
            await _unitOfWork.BookShelfRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<BookShelf> GetBookShelf(int id)
        {
            return await _unitOfWork.BookShelfRepository.GetById(id);
        }

        public PagedList<BookShelf> GetBookShelves(BookShelfQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var bookShelves = _unitOfWork.BookShelfRepository.GetAll();
            if (filters.LocationId != null)
            {
                bookShelves = bookShelves.Where(x => x.LocationId == filters.LocationId);
            }
            var pagedBookShelves = PagedList<BookShelf>.Create(bookShelves, filters.PageNumber, filters.PageSize);
            return pagedBookShelves;
        }

        public async Task InsertBookShelf(BookShelf bookShelf)
        {
            await _unitOfWork.BookShelfRepository.Add(bookShelf);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateBookShelf(BookShelf bookShelf)
        {
            _unitOfWork.BookShelfRepository.Update(bookShelf);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
