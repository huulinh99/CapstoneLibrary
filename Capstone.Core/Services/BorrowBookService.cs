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
        public async Task<bool> DeleteBorrowBook(int?[] id)
        {
            await _unitOfWork.BorrowBookRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
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
            if (filters.StaffId != null)
            {
                borrowBooks = borrowBooks.Where(x => x.StaffId == filters.StaffId);
            }
            var pagedBorrowBooks = PagedList<BorrowBookDto>.Create(borrowBooks, filters.PageNumber, filters.PageSize);
            return pagedBorrowBooks;
        }

        public async Task<BorrowBook> GetBorrowBook(int id)
        {
            return await _unitOfWork.BorrowBookRepository.GetById(id);
        }

        public async Task InsertBorrowBook(BorrowBook borrowBook)
        {
            var customer = await _unitOfWork.CustomerRepository.GetById(borrowBook.CustomerId);
            if (customer == null)
            {
                throw new BusinessException("User doesn't exist");
            }

            List<BookGroupDto> bookGroups = new List<BookGroupDto>();
            var borrowDetails = borrowBook.BorrowDetail;
            foreach (var borrowDetail in borrowDetails)
            {
                borrowDetail.Fee = borrowDetail.Book.BookGroup.Fee * (borrowBook.StartTime - borrowBook.EndTime).Ticks;
            }
            await _unitOfWork.BorrowBookRepository.Add(borrowBook);          
            foreach (var borrowDetail in borrowDetails)
            {
                var bookGroup = _unitOfWork.BookGroupRepository.GetBookGroupsByBookId(borrowDetail.BookId);
                bookGroups.Add(bookGroup);
            }
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
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateBorrowBook(BorrowBook borrowBook)
        {
            _unitOfWork.BorrowBookRepository.Update(borrowBook);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
