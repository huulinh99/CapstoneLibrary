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
            var borrowBooks = _unitOfWork.BorrowBookRepository.GetAllBorrowBookWithPatronName();
            if (filters.PatronId != null)
            {
                borrowBooks = borrowBooks.Where(x => x.PatronId == filters.PatronId);
            }

            if (filters.IsNewest == true)
            {
                borrowBooks = borrowBooks.OrderByDescending(x => x.Id).Take(5);
            }
            if (filters.PatronName != null)
            {
                borrowBooks = borrowBooks.Where(x => x.PatronName.ToLower().Contains(filters.PatronName.ToLower()));
            }
            if (filters.ReturnToday != null)
            {
                borrowBooks = _unitOfWork.BorrowBookRepository.GetBorrowBookReturnToday();
                //Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd"));
                //borrowBooks = borrowBooks.Where(x => x.EndTime == filters.ReturnToday);
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
            var patron = _unitOfWork.PatronRepository.GetById(borrowBook.PatronId);
            if (patron == null)
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
                borrowDetail.PunishFee = bg.PunishFee;
                var book = _unitOfWork.BookRepository.GetById(borrowDetail.BookId);
                book.IsAvailable = false;
                borrowDetail.IsDeleted = false;
                borrowDetail.IsReturn = false;
                _unitOfWork.BookRepository.Update(book);
                _unitOfWork.SaveChanges();
                bookGroups.Add(bg);
            }
            if (borrowBook.EndTime == null)
            {
                borrowBook.EndTime = borrowBook.StartTime.AddDays(7);
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
            //tim duoc favourite category cua tung patron
            var favouriteCategories = _unitOfWork.FavouriteCategoryRepository.GetFavouriteCategoryByUser(borrowBook.PatronId);
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
                            PatronId = borrowBook.PatronId,
                            CategoryId = category.Id,
                            IsDeleted = false,
                            Rating = 1
                        };
                        _unitOfWork.FavouriteCategoryRepository.Add(entity);
                        _unitOfWork.SaveChanges();
                        favouriteCategories = _unitOfWork.FavouriteCategoryRepository.GetFavouriteCategoryByUser(borrowBook.PatronId);
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

        public IEnumerable<BorrowBookDto> GetReturnToday()
        {
            return _unitOfWork.BorrowBookRepository.GetBorrowBookReturnToday();
        }
    }
}
