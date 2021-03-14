using Capstone.Core.CustomEntities;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Core.Interfaces.DetectionErrorInterfaces;
using Capstone.Core.QueryFilters;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.Services
{
    public class DetectionErrorService : IDetectionErrorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public DetectionErrorService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public bool DeleteDetectionError(int?[] id)
        {
            _unitOfWork.DetectionErrorRepository.Delete(id);
            _unitOfWork.SaveChanges();
            return true;
        }

        public PagedList<DetectionError> GetDetectionErrors(DetectionErrorQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var detectionErrors = _unitOfWork.DetectionErrorRepository.GetAll();

            var pagedDetectionErrors = PagedList<DetectionError>.Create(detectionErrors, filters.PageNumber, filters.PageSize);
            return pagedDetectionErrors;
        }

        public DetectionError GetDetectionError(int id)
        {
            return _unitOfWork.DetectionErrorRepository.GetById(id);
        }

        public void InsertDetectionError(DetectionError detectionError)
        {
            _unitOfWork.DetectionErrorRepository.Add(detectionError);
            _unitOfWork.SaveChanges();
        }

        public bool UpdateDetectionError(DetectionError detectionError)
        {
            _unitOfWork.DetectionErrorRepository.Update(detectionError);
            _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
