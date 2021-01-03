using Capstone.Core.CustomEntities;
using Capstone.Core.Entities;
using Capstone.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces
{
    public interface IDeviceService
    {
        PagedList<Device> GetDevices(DeviceQueryFilter filters);
        Task<Device> GetDevice(int id);
        Task InsertDevice(Device device);
        Task<bool> UpdateDevice(Device device);
        Task<bool> DeleteDevice(int[] id);
    }
}
