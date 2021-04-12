using AutoMapper;
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
    public class BorrowDetailService : IBorrowDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PaginationOptions _paginationOptions;
        public BorrowDetailService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
            _mapper = mapper;
        }
        public bool DeleteBorrowDetail(int?[] id)
        {
            _unitOfWork.BorrowDetailRepository.Delete(id);
            _unitOfWork.SaveChanges();
            return true;
        }

        public BorrowDetail GetBorrowDetail(int id)
        {
            return _unitOfWork.BorrowDetailRepository.GetById(id);
        }

        public PagedList<BorrowDetailDto> GetBorrowDetails(BorrowDetailQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var borrowDetails = _unitOfWork.BorrowDetailRepository.GetAllBorrowDetailAndBookName();
            var borrowBooks = _unitOfWork.BorrowBookRepository.GetAllBorrowBookWithCustomerName();

            if(filters.CustomerId != null)
            {
                borrowBooks = borrowBooks.Where(x => x.CustomerId == filters.CustomerId);
                borrowDetails = _unitOfWork.BorrowDetailRepository.GetBorrowDetailWithListBorrow(borrowBooks);
            }
            if (filters.BorrowId != null)
            {
                borrowDetails = borrowDetails.Where(x => x.BorrowId == filters.BorrowId);
            }

            if (filters.Barcode != null)
            {
                var borrowId = borrowDetails.Where(x => x.Barcode == filters.Barcode).Last().BorrowId;
                borrowDetails = borrowDetails.Where(x => x.BorrowId == borrowId);
            }

            if (filters.BookId != null)
            {
                borrowDetails = borrowDetails.Where(x => x.BookId == filters.BookId);
            }
            var pagedBorrowDetails = PagedList<BorrowDetailDto>.Create(borrowDetails, filters.PageNumber, filters.PageSize);
            return pagedBorrowDetails;
        }

        public void InsertBorrowDetail(BorrowDetail borrowDetail)
        {
            _unitOfWork.BorrowDetailRepository.Add(borrowDetail);
            _unitOfWork.SaveChanges();
        }

        public bool UpdateBorrowDetail(BorrowDetail borrowDetail)
        {
            _unitOfWork.BorrowDetailRepository.Update(borrowDetail);
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
