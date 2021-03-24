using Capstone.Core.CustomEntities;
using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Core.Interfaces.UndefinedErrorInterfaces;
using Capstone.Core.QueryFilters;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capstone.Core.Services
{
    public class UndefinedErrorService : IUndefinedErrorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public UndefinedErrorService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public bool DeleteUndefinedError(int?[] id)
        {
            _unitOfWork.UndefinedErrorRepository.Delete(id);
            _unitOfWork.SaveChanges();
            return true;
        }

        public IEnumerable<UndefinedErrorDto> GetUndefinedErrors(UndefinedErrorQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var undefinedErrors = _unitOfWork.UndefinedErrorRepository.GetAllUndefinedError();
            if (filters.DrawerDetectionId != null)
            {
                undefinedErrors = undefinedErrors.Where(x => x.DrawerDetectionId == filters.DrawerDetectionId);
            }

            if (filters.IsConfirm != null)
            {
                undefinedErrors = undefinedErrors.Where(x => x.IsConfirm == filters.IsConfirm);
            }
            return undefinedErrors;
        }

        public UndefinedError GetUndefinedError(int id)
        {
            return _unitOfWork.UndefinedErrorRepository.GetById(id);
        }

        public void InsertUndefinedError(UndefinedError undefinedError)
        {
            _unitOfWork.UndefinedErrorRepository.Add(undefinedError);
            _unitOfWork.SaveChanges();
        }

        public bool UpdateUndefinedError(UndefinedError undefinedError)
        {
            _unitOfWork.UndefinedErrorRepository.Update(undefinedError);
            _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
