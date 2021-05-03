using Capstone.Core.CustomEntities;
using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Exceptions;
using Capstone.Core.Interfaces;
using Capstone.Core.QueryFilters;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Services
{
    public class ReturnBookService : IReturnBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public ReturnBookService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public bool DeleteReturnBook(int?[] id)
        {
            _unitOfWork.ReturnBookRepository.Delete(id);
            _unitOfWork.SaveChanges();
            return true;
        }

        public ReturnBookDto GetReturnBook(int id)
        {
            return _unitOfWork.ReturnBookRepository.GetReturnById(id);
        }

        public PagedList<ReturnBookDto> GetReturnBooks(ReturnBookQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var returnBooks = _unitOfWork.ReturnBookRepository.GetAllReturnBookWithPatronName();
            if (filters.BorrowId != null)
            {
                returnBooks = returnBooks.Where(x => x.BorrowId == filters.BorrowId);
            }
            if (filters.PatronId != null)
            {
                returnBooks = returnBooks.Where(x => x.PatronId == filters.PatronId);
            }
            if (filters.PatronName != null)
            {
                returnBooks = returnBooks.Where(x => x.PatronName.ToLower().Contains(filters.PatronName.ToLower()));
            }
            if (filters.ByMonth != null)
            {
                returnBooks = _unitOfWork.ReturnBookRepository.GetAllReturnGroupByMonth();
            }
            if (filters.ReturnTime != null)
            {
                returnBooks = returnBooks.Where(x => x.ReturnTime == filters.ReturnTime);
            }
            if (filters.StaffId != null)
            {
                returnBooks = returnBooks.Where(x => x.StaffId == filters.StaffId);
            }
            var pagedReturnBooks = PagedList<ReturnBookDto>.Create(returnBooks, filters.PageNumber, filters.PageSize);
            return pagedReturnBooks;
        }

        public void InsertReturnBook(ReturnBook returnBook)
        {
            var patron = _unitOfWork.PatronRepository.GetById(returnBook.PatronId);
            if (patron == null)
            {
                throw new BusinessException("User doesn't exist");
            }
            foreach (var returnDetail in returnBook.ReturnDetail)
            {
                var book = _unitOfWork.BookRepository.GetById(returnDetail.BookId);
                var bookGroupId = _unitOfWork.BookRepository.GetById(returnDetail.BookId).BookGroupId;
                var bg = _unitOfWork.BookGroupRepository.GetById(bookGroupId);
                var startTime = _unitOfWork.BorrowBookRepository.GetById(returnBook.BorrowId).StartTime;
                var borrow = _unitOfWork.BorrowBookRepository.GetById(returnBook.BorrowId);
                var borrowDetails = _unitOfWork.BorrowDetailRepository.GetAllBorrowDetail(borrow.Id);
                foreach (var borrowDetail in borrowDetails)
                {
                    if (borrowDetail.BookId == returnDetail.BookId)
                    {
                        borrowDetail.IsReturn = true;
                        //var timeReturn = (borrow.EndTime - borrow.StartTime).Days;
                        //returnDetail.Fee = borrowDetail.Fee * (timeReturn + 1);
                        //(returnBook.ReturnTime.)
                        if(returnBook.ReturnTime < borrow.EndTime)
                        {
                            var timeReturn = (returnBook.ReturnTime - borrow.StartTime).Days;
                            returnDetail.Fee = borrowDetail.Fee * (timeReturn + 1);
                        }

                        if (returnBook.ReturnTime >= borrow.EndTime)
                        {
                            var timeReturn = (borrow.EndTime - borrow.StartTime).Days;
                            returnDetail.Fee = borrowDetail.Fee * (timeReturn + 1);
                            returnDetail.PunishFee = (double)borrowDetail.PunishFee * ((returnBook.ReturnTime - borrow.EndTime).Days);
                            returnDetail.IsLate = true;
                        }
                        returnBook.Fee += (returnDetail.Fee + returnDetail.PunishFee);
                        //Debug.WriteLine((returnBook.ReturnTime - startTime).Days);
                        returnDetail.IsDeleted = false;
                        _unitOfWork.BorrowDetailRepository.Update(borrowDetail);
                        _unitOfWork.SaveChanges();
                    }
                }
                
                book.IsAvailable = true;
                _unitOfWork.BookRepository.Update(book);
                _unitOfWork.SaveChanges();
            }
            _unitOfWork.ReturnBookRepository.Add(returnBook);
            _unitOfWork.SaveChanges();
        }

        public bool UpdateReturnBook(ReturnBook returnBook)
        {
            _unitOfWork.ReturnBookRepository.Update(returnBook);
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
