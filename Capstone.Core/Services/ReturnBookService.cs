﻿using Capstone.Core.CustomEntities;
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

        public ReturnBook GetReturnBook(int id)
        {
            return _unitOfWork.ReturnBookRepository.GetById(id);
        }

        public PagedList<ReturnBookDto> GetReturnBooks(ReturnBookQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var returnBooks = _unitOfWork.ReturnBookRepository.GetAllReturnBookWithCustomerName();
            if (filters.BorrowId != null)
            {
                returnBooks = returnBooks.Where(x => x.BorrowId == filters.BorrowId);
            }
            if (filters.CustomerId != null)
            {
                returnBooks = returnBooks.Where(x => x.CustomerId == filters.CustomerId);
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
            var customer = _unitOfWork.CustomerRepository.GetById(returnBook.CustomerId);
            if (customer == null)
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
                    if(borrowDetail.BookId == returnDetail.BookId)
                    {
                        borrowDetail.IsReturn = true;
                    }
                }
                returnDetail.Fee = bg.Fee * (returnBook.ReturnTime - startTime).Days;
                if((returnBook.ReturnTime - startTime).Days > 7)
                {
                    returnDetail.PunishFee = bg.PunishFee * ((returnBook.ReturnTime - startTime).Days-7);
                    returnDetail.IsLate = true; 
                }
                returnBook.Fee += (returnDetail.Fee + returnDetail.PunishFee);
                //Debug.WriteLine((returnBook.ReturnTime - startTime).Days);
                returnDetail.IsDeleted = false;
                book.IsAvailable = true;
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
