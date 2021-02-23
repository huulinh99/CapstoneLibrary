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
        Staff GetStaff(int id);
        void InsertStaff(Staff staff);
        Staff GetStaffByUserName(string username);
        bool UpdateStaff(Staff staff);
        bool DeleteStaff(int?[] id);
        StaffDto GetLoginByCredenticals(UserLogin login);
    }
}
