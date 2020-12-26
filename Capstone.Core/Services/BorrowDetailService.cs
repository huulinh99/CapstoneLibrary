using AutoMapper;
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
        public async Task<bool> DeleteBorrowDetail(int id)
        {
            await _unitOfWork.BorrowDetailRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<BorrowDetail> GetBorrowDetail(int id)
        {
            return await _unitOfWork.BorrowDetailRepository.GetById(id);
        }

        public  PagedList<BorrowDetail> GetBorrowDetails(BorrowDetailQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var borrowDetails = _unitOfWork.BorrowDetailRepository.GetAll();
            if (filters.BorrowId != null)
            {
                borrowDetails = borrowDetails.Where(x => x.BorrowId == filters.BorrowId);
            }

            if (filters.BookId != null)
            {
                borrowDetails = borrowDetails.Where(x => x.BookId == filters.BookId);
            }
            var pagedBorrowDetails = PagedList<BorrowDetail>.Create(borrowDetails, filters.PageNumber, filters.PageSize);
            return pagedBorrowDetails;
        }

        public async Task InsertBorrowDetail(BorrowDetail borrowDetail)
        {
            await _unitOfWork.BorrowDetailRepository.Add(borrowDetail);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateBorrowDetail(BorrowDetail borrowDetail)
        {
            _unitOfWork.BorrowDetailRepository.Update(borrowDetail);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
