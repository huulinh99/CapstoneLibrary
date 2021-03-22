using Capstone.Core.CustomEntities;
using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Core.Interfaces.DrawerDetectionInterfaces;
using Capstone.Core.QueryFilters;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capstone.Core.Services
{
    public class DrawerDetectionService : IDrawerDetectionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public DrawerDetectionService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public bool DeleteDrawerDetection(int?[] id)
        {
            _unitOfWork.DrawerDetectionRepository.Delete(id);
            _unitOfWork.SaveChanges();
            return true;
        }

        public IEnumerable<DrawerDetectionDto> GetDrawerDetections(DrawerDetectionQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var drawerDetections = _unitOfWork.DrawerDetectionRepository.GetAllDrawerDetection();
            if (filters.DetectionId != null)
            {
                drawerDetections = drawerDetections.Where(x => x.DetectionId == filters.DetectionId);
            }
            return drawerDetections;
        }

        public DrawerDetection GetDrawerDetection(int id)
        {
            return _unitOfWork.DrawerDetectionRepository.GetById(id);
        }

        public void InsertDrawerDetection(DrawerDetection drawerDetection)
        {
            _unitOfWork.DrawerDetectionRepository.Add(drawerDetection);
            _unitOfWork.SaveChanges();
        }

        public bool UpdateDrawerDetection(DrawerDetection drawerDetection)
        {
            _unitOfWork.DrawerDetectionRepository.Update(drawerDetection);
            _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
