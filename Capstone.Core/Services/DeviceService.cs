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

        public async Task<bool> DeleteDevice(int?[] id)
        {
            await _unitOfWork.DeviceRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<Device> GetDevice(int id)
        {
            return await _unitOfWork.DeviceRepository.GetById(id);
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

        public async Task InsertDevice(Device device)
        {
            await _unitOfWork.DeviceRepository.Add(device);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateDevice(Device device)
        {
            _unitOfWork.DeviceRepository.Update(device);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
