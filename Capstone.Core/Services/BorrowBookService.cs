using AutoMapper;
using Capstone.Core.CustomEntities;
using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Exceptions;
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
    public class BorrowBookService : IBorrowBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PaginationOptions _paginationOptions;
        public BorrowBookService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
            _mapper = mapper;
        }
        public bool DeleteBorrowBook(int?[] id)
        {
            _unitOfWork.BorrowBookRepository.Delete(id);
            _unitOfWork.SaveChanges();
            return true;
        }

        public PagedList<BorrowBookDto> GetBorrowBooks(BorrowBookQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var borrowBooks = _unitOfWork.BorrowBookRepository.GetAllBorrowBookWithCustomerName();
            if (filters.CustomerId != null)
            {
                borrowBooks = borrowBooks.Where(x => x.CustomerId == filters.CustomerId);
            }

            if (filters.CustomerName != null)
            {
                borrowBooks = borrowBooks.Where(x => x.CustomerName.ToLower().Contains(filters.CustomerName.ToLower()));
            }

            if (filters.StaffId != null)
            {
                borrowBooks = borrowBooks.Where(x => x.StaffId == filters.StaffId);
            }
            var pagedBorrowBooks = PagedList<BorrowBookDto>.Create(borrowBooks, filters.PageNumber, filters.PageSize);
            return pagedBorrowBooks;
        }

        public BorrowBook GetBorrowBook(int id)
        {
            return _unitOfWork.BorrowBookRepository.GetById(id);
        }

        public void InsertBorrowBook(BorrowBook borrowBook)
        {
            var customer = _unitOfWork.CustomerRepository.GetById(borrowBook.CustomerId);
            if (customer == null)
            {
                throw new BusinessException("User doesn't exist");
            }

            List<BookGroup> bookGroups = new List<BookGroup>();
            var borrowDetails = borrowBook.BorrowDetail;
            foreach (var borrowDetail in borrowDetails)
            {
                var bookGroupId = _unitOfWork.BookRepository.GetById(borrowDetail.BookId).BookGroupId;
                var bg = _unitOfWork.BookGroupRepository.GetById(bookGroupId);
                var fee = bg.Fee;
                borrowDetail.Fee = fee * (borrowBook.EndTime - borrowBook.StartTime).Ticks;
                var book = _unitOfWork.BookRepository.GetById(borrowDetail.BookId);
                book.IsAvailable = false;
                bookGroups.Add(bg);
            }
            _unitOfWork.BorrowBookRepository.Add(borrowBook);          
            List<IEnumerable<BookCategory>> bookCategories = new List<IEnumerable<BookCategory>>();
            foreach (var bookGroup in bookGroups)
            {
                var bookCategory = _unitOfWork.BookCategoryRepository.GetBookCategoriesByBookGroup(bookGroup.Id);
                bookCategories.Add(bookCategory);
            }

            List<IEnumerable<CategoryDto>> listCategories = new List<IEnumerable<CategoryDto>>();
            foreach (var bookCategory in bookCategories)
            {
                var category = _unitOfWork.CategoryRepository.GetCategoryNameByBookCategory(bookCategory);
                listCategories.Add(category);
            }

            var favouriteCategories = _unitOfWork.FavouriteCategoryRepository.GetFavouriteCategoryByUser(borrowBook.CustomerId);
            foreach (var listCategory in listCategories)
            {
                foreach (var category in listCategory)
                {
                    foreach (var favouriteCategory in favouriteCategories)
                    {
                        if (category.Id == favouriteCategory.Id)
                        {
                            favouriteCategory.Rating += 1;
                            _unitOfWork.FavouriteCategoryRepository.Update(favouriteCategory);
                        }
                    }
                    
                }
            }
            _unitOfWork.SaveChanges();
        }

        public bool UpdateBorrowBook(BorrowBook borrowBook)
        {
            _unitOfWork.BorrowBookRepository.Update(borrowBook);
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
