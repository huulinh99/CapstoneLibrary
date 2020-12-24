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
    public class ErrorMessageService : IErrorMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public ErrorMessageService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public async Task<bool> DeleteErrorMessage(int id)
        {
            await _unitOfWork.ErrorMessageRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<ErrorMessage> GetErrorMessage(int id)
        {
            return await _unitOfWork.ErrorMessageRepository.GetById(id);
        }

        public PagedList<ErrorMessage> GetErrorMessages(ErrorMessageQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var errorMessages = _unitOfWork.ErrorMessageRepository.GetAll();
            if (filters.BookDetectErrorId != null)
            {
                errorMessages = errorMessages.Where(x => x.BookDetectErrorId == filters.BookDetectErrorId);
            }
            var pagedErrorMessages = PagedList<ErrorMessage>.Create(errorMessages, filters.PageNumber, filters.PageSize);
            return pagedErrorMessages;
        }

        public async Task InsertErrorMessage(ErrorMessage errorMessage)
        {
            await _unitOfWork.ErrorMessageRepository.Add(errorMessage);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateErrorMessage(ErrorMessage errorMessage)
        {
            _unitOfWork.ErrorMessageRepository.Update(errorMessage);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
