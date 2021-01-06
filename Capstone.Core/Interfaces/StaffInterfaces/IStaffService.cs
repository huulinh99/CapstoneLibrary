using Capstone.Core.CustomEntities;
using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces
{
    public interface IStaffService
    {
        PagedList<Staff> GetStaffs(StaffQueryFilter filters);
        Task<Staff> GetStaff(int id);
        Task InsertStaff(Staff staff);
        Task<bool> UpdateStaff(Staff staff);
        Task<bool> DeleteStaff(int?[] id);
        Task<StaffDto> GetLoginByCredenticals(UserLogin login);
    }
}
