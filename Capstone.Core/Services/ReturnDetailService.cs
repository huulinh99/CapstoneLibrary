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
    public class ReturnDetailService : IReturnDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public ReturnDetailService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public async Task<bool> DeleteReturnDetail(int[] id)
        {
            await _unitOfWork.ReturnDetailRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<ReturnDetail> GetReturnDetail(int id)
        {
            return await _unitOfWork.ReturnDetailRepository.GetById(id);
        }

        public PagedList<ReturnDetail> GetReturnDetails(ReturnDetailQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var returnDetails = _unitOfWork.ReturnDetailRepository.GetAll();
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
            var pagedReturnDetails = PagedList<ReturnDetail>.Create(returnDetails, filters.PageNumber, filters.PageSize);
            return pagedReturnDetails;
        }

        public async Task InsertReturnDetail(ReturnDetail returnDetail)
        {
            await _unitOfWork.ReturnDetailRepository.Add(returnDetail);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateReturnDetail(ReturnDetail returnDetail)
        {
            _unitOfWork.ReturnDetailRepository.Update(returnDetail);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
