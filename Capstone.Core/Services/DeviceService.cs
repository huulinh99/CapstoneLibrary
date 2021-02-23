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
    public class DeviceService : IDeviceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public DeviceService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public bool DeleteDevice(int?[] id)
        {
            _unitOfWork.DeviceRepository.Delete(id);
            _unitOfWork.SaveChanges();
            return true;
        }

        public Device GetDevice(int id)
        {
            return _unitOfWork.DeviceRepository.GetById(id);
        }

        public PagedList<Device> GetDevices(DeviceQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var devices = _unitOfWork.DeviceRepository.GetAll();
            if (filters.CustomerId != null)
            {
                devices = devices.Where(x => x.CustomerId == filters.CustomerId);
            }
            if (filters.DeviceToken != null)
            {
                devices = devices.Where(x => x.DeviceToken == filters.DeviceToken);
            }
            var pagedDevices = PagedList<Device>.Create(devices, filters.PageNumber, filters.PageSize);
            return pagedDevices;
        }

        public void InsertDevice(Device device)
        {
            _unitOfWork.DeviceRepository.Add(device);
            _unitOfWork.SaveChanges();
        }

        public bool UpdateDevice(Device device)
        {
            _unitOfWork.DeviceRepository.Update(device);
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
