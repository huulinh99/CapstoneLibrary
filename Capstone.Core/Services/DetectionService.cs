﻿using Capstone.Core.CustomEntities;
using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Core.Interfaces.DetectionInterfaces;
using Capstone.Core.QueryFilters;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capstone.Core.Services
{
    public class DetectionService : IDetectionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public DetectionService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public bool DeleteDetection(int?[] id)
        {
            _unitOfWork.DetectionRepository.Delete(id);
            _unitOfWork.SaveChanges();
            return true;
        }

        public PagedList<DetectionDto> GetDetections(DetectionQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var detections = _unitOfWork.DetectionRepository.GetAllDetection();
            if (filters.BookShelfName != null)
            {
                detections = detections.Where(x => x.BookShelfName.ToLower().Contains(filters.BookShelfName.ToLower()));
            }

            if(filters.StartTime!=null && filters.EndTime != null)
            {
                detections = detections.Where(x => x.Time<filters.EndTime && x.Time > filters.StartTime);
            }

            if (filters.Time!=null)
            {
                detections = detections.Where(x => x.Time == filters.Time);
            }

            var pagedDetections = PagedList<DetectionDto>.Create(detections, filters.PageNumber, filters.PageSize);
            return pagedDetections;
        }

        public Detection GetDetection(int id)
        {
            return _unitOfWork.DetectionRepository.GetById(id);
        }

        public void InsertDetection(Detection detection)
        {
            _unitOfWork.DetectionRepository.Add(detection);
            foreach (var drawerDetection in detection.DrawerDetection)
            {
                foreach (var detectionError in drawerDetection.DetectionError)
                {
                    detectionError.IsRejected = false;
                    detectionError.IsConfirm = false;
                }

                foreach (var undefinedError in drawerDetection.UndefinedError)
                {
                    undefinedError.IsRejected = false;
                    undefinedError.IsConfirm = false;
                }
            }
            _unitOfWork.SaveChanges();
        }

        public bool UpdateDetection(Detection detection)
        {
            _unitOfWork.DetectionRepository.Update(detection);
            _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}