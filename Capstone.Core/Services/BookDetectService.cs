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
    public class BookDetectService : IBookDetectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public BookDetectService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public async Task<bool> DeleteBookDetect(int id)
        {
            await _unitOfWork.BookDetectRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public PagedList<BookDetect> GetBookDetect(BookDetectQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var bookDetects = _unitOfWork.BookDetectRepository.GetAll();
            if (filters.BookId != null)
            {
                bookDetects = bookDetects.Where(x => x.BookId == filters.BookId);
            }

            if (filters.IsError != null)
            {
                bookDetects = bookDetects.Where(x => x.IsError == filters.IsError);
            }

            if (filters.StaffId != null)
            {
                bookDetects = bookDetects.Where(x => x.StaffId == filters.StaffId);
            }

            var pagedBookDetects = PagedList<BookDetect>.Create(bookDetects, filters.PageNumber, filters.PageSize);
            return pagedBookDetects;
        }

        public async Task<BookDetect> GetBookDetect(int id)
        {
            return await _unitOfWork.BookDetectRepository.GetById(id);
        }

        public async Task InsertBookDetect(BookDetect bookDetect)
        {
            await _unitOfWork.BookDetectRepository.Add(bookDetect);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateBookDetect(BookDetect bookDetect)
        {
            _unitOfWork.BookDetectRepository.Update(bookDetect);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
