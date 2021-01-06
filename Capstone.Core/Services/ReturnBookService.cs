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
    public class ReturnBookService : IReturnBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public ReturnBookService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public async Task<bool> DeleteReturnBook(int?[] id)
        {
            await _unitOfWork.ReturnBookRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<ReturnBook> GetReturnBook(int id)
        {
            return await _unitOfWork.ReturnBookRepository.GetById(id);
        }

        public PagedList<ReturnBook> GetReturnBooks(ReturnBookQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var returnBooks = _unitOfWork.ReturnBookRepository.GetAll();
            if (filters.BorrowId != null)
            {
                returnBooks = returnBooks.Where(x => x.BorrowId == filters.BorrowId);
            }
            if (filters.CustomerId != null)
            {
                returnBooks = returnBooks.Where(x => x.CustomerId == filters.CustomerId);
            }
            if (filters.ReturnTime != null)
            {
                returnBooks = returnBooks.Where(x => x.ReturnTime == filters.ReturnTime);
            }
            if (filters.StaffId != null)
            {
                returnBooks = returnBooks.Where(x => x.StaffId == filters.StaffId);
            }
            var pagedReturnBooks = PagedList<ReturnBook>.Create(returnBooks, filters.PageNumber, filters.PageSize);
            return pagedReturnBooks;
        }

        public async Task InsertReturnBook(ReturnBook returnBook)
        {
            await _unitOfWork.ReturnBookRepository.Add(returnBook);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateReturnBook(ReturnBook returnBook)
        {
            _unitOfWork.ReturnBookRepository.Update(returnBook);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
