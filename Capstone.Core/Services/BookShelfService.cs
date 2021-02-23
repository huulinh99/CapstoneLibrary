using Capstone.Core.CustomEntities;
using Capstone.Core.DTOs;
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

        public bool DeleteBookShelf(int?[] id)
        {
            _unitOfWork.BookShelfRepository.Delete(id);
            var bookShelfId = _unitOfWork.BookShelfRepository.GetBookShelfIdInLocation(id);
            _unitOfWork.DrawerRepository.DeleteDrawerInBookShelf(bookShelfId.ToArray());
            _unitOfWork.SaveChangesAsync();
            return true;
        }

        public BookShelf GetBookShelf(int id)
        {
            return _unitOfWork.BookShelfRepository.GetById(id);
        }

        public PagedList<BookShelfDto> GetBookShelves(BookShelfQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var bookShelves = _unitOfWork.BookShelfRepository.GetBookShelvesAndLocationName();
            if (filters.LocationId != null)
            {
                bookShelves = bookShelves.Where(x => x.LocationId == filters.LocationId);
            }

            if (filters.BookGroupId != null)
            {
                var books = _unitOfWork.BookRepository.GetBookByBookGroup(filters.BookGroupId);
                var bookDrawers = _unitOfWork.BookDrawerRepository.GetBookDrawerByListBook(books);
                var drawers = _unitOfWork.DrawerRepository.GetAllDrawers(bookDrawers);
                bookShelves = _unitOfWork.BookShelfRepository.GetBookShelvesByDrawer(drawers);
            }

            if (filters.Name != null)
            {
                bookShelves = bookShelves.Where(x => x.Name.ToLower().Contains(filters.Name.ToLower()));
            }
            var pagedBookShelves = PagedList<BookShelfDto>.Create(bookShelves, filters.PageNumber, filters.PageSize);
            return pagedBookShelves;
        }

        public void InsertBookShelf(BookShelf bookShelf)
        {
            _unitOfWork.BookShelfRepository.Add(bookShelf);
            _unitOfWork.SaveChangesAsync();
            for (int i = 1; i <= bookShelf.Row; i++)
            {
                for (int j = 1; j <= bookShelf.Col; j++)
                {
                    var drawerModel = new Drawer()
                    {
                        BookShelfId = bookShelf.Id,
                        ShelfRow = i,
                        ShelfColumn = j
                    };
                    _unitOfWork.DrawerRepository.Add(drawerModel);
                }                          
            }

            _unitOfWork.SaveChangesAsync();
        }

        public bool UpdateBookShelf(BookShelf bookShelf)
        {
            bookShelf.IsDeleted = false;
            _unitOfWork.BookShelfRepository.Update(bookShelf);
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
