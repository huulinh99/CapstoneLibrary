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
        public bool DeleteErrorMessage(int?[] id)
        {
            _unitOfWork.ErrorMessageRepository.Delete(id);
            _unitOfWork.SaveChanges();
            return true;
        }

        public ErrorMessage GetErrorMessage(int id)
        {
            return _unitOfWork.ErrorMessageRepository.GetById(id);
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

        public void InsertErrorMessage(ErrorMessage errorMessage)
        {
            _unitOfWork.ErrorMessageRepository.Add(errorMessage);
            _unitOfWork.SaveChanges();
        }

        public bool UpdateErrorMessage(ErrorMessage errorMessage)
        {
            _unitOfWork.ErrorMessageRepository.Update(errorMessage);
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
