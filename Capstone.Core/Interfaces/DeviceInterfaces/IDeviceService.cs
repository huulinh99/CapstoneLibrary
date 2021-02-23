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
        Device GetDevice(int id);
        void InsertDevice(Device device);
        bool UpdateDevice(Device device);
        bool DeleteDevice(int?[] id);
    }
}
