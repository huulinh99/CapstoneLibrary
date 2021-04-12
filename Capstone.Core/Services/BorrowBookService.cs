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

            if (filters.IsNewest == true)
            {
                borrowBooks = borrowBooks.OrderByDescending(x => x.Id).Take(5);
            }
            if (filters.CustomerName != null)
            {
                borrowBooks = borrowBooks.Where(x => x.CustomerName.ToLower().Contains(filters.CustomerName.ToLower()));
            }
            if (filters.ReturnToday != null)
            {
                borrowBooks = borrowBooks.Where(x => x.EndTime == filters.ReturnToday);
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
                borrowDetail.Fee = bg.Fee;
                var book = _unitOfWork.BookRepository.GetById(borrowDetail.BookId);
                book.IsAvailable = false;
                borrowDetail.IsDeleted = false;
                borrowDetail.IsReturn = false;
                bookGroups.Add(bg);
            }        
            _unitOfWork.BorrowBookRepository.Add(borrowBook);
            List<IEnumerable<BookCategory>> bookCategories = new List<IEnumerable<BookCategory>>();
            foreach (var bookGroup in bookGroups)
            {
                var bookCategory = _unitOfWork.BookCategoryRepository.GetBookCategoriesByBookGroup(bookGroup.Id);
                //tim dc book category cua tung book group
                bookCategories.Add(bookCategory);
            }

            List<IEnumerable<CategoryDto>> listCategories = new List<IEnumerable<CategoryDto>>();
            foreach (var bookCategory in bookCategories)
            {
                var category = _unitOfWork.CategoryRepository.GetCategoryNameByBookCategory(bookCategory);
                //tim duoc category cu the 
                listCategories.Add(category);
            }
            //tim duoc favourite category cua tung customer
            var favouriteCategories = _unitOfWork.FavouriteCategoryRepository.GetFavouriteCategoryByUser(borrowBook.CustomerId);
            foreach (var listCategory in listCategories)
            {
                foreach (var category in listCategory)
                {
                    if (favouriteCategories.Any(x => x.CategoryId == category.Id))
                    {
                        var tmp = favouriteCategories.Where(x => x.CategoryId == category.Id).FirstOrDefault();
                        tmp.Rating += 1;
                        _unitOfWork.FavouriteCategoryRepository.Update(tmp);
                        _unitOfWork.SaveChanges();
                    }
                    else
                    {
                        var entity = new FavouriteCategory
                        {
                            CustomerId = borrowBook.CustomerId,
                            CategoryId = category.Id,
                            IsDeleted = false,
                            Rating = 1
                        };
                        _unitOfWork.FavouriteCategoryRepository.Add(entity);
                        _unitOfWork.SaveChanges();
                        favouriteCategories = _unitOfWork.FavouriteCategoryRepository.GetFavouriteCategoryByUser(borrowBook.CustomerId);
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
