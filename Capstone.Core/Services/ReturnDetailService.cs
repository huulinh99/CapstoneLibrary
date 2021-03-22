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
    public class ReturnDetailService : IReturnDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public ReturnDetailService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public bool DeleteReturnDetail(int?[] id)
        {
            _unitOfWork.ReturnDetailRepository.Delete(id);
            _unitOfWork.SaveChanges();
            return true;
        }

        public ReturnDetail GetReturnDetail(int id)
        {
            return _unitOfWork.ReturnDetailRepository.GetById(id);
        }

        public PagedList<ReturnDetailDto> GetReturnDetails(ReturnDetailQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var returnDetails = _unitOfWork.ReturnDetailRepository.GetAllReturnDetailWithBookName();
            if(filters.CustomerId != null)
            {
                returnDetails = returnDetails.Where(x => x.CustomerId == filters.CustomerId);
            }

            if (filters.ByMonth != null)
            {
                
            }

            if (filters.BookId != null)
            {
                returnDetails = returnDetails.Where(x => x.BookId == filters.BookId);
            }
            if (filters.ReturnId != null)
            {
                returnDetails = returnDetails.Where(x => x.ReturnId == filters.ReturnId);
            }
            if (filters.Fee != null)
            {
                returnDetails = returnDetails.Where(x => x.Fee == filters.Fee);
            }
            if (filters.PunishFee != null)
            {
                returnDetails = returnDetails.Where(x => x.PunishFee == filters.PunishFee);
            }
            var pagedReturnDetails = PagedList<ReturnDetailDto>.Create(returnDetails, filters.PageNumber, filters.PageSize);
            return pagedReturnDetails;
        }

        public void InsertReturnDetail(ReturnDetail returnDetail)
        {
            _unitOfWork.ReturnDetailRepository.Add(returnDetail);
            _unitOfWork.SaveChanges();
        }

        public bool UpdateReturnDetail(ReturnDetail returnDetail)
        {
            _unitOfWork.ReturnDetailRepository.Update(returnDetail);
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
