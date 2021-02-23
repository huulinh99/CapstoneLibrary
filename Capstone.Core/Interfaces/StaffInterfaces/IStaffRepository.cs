using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces
{
    public interface IStaffRepository : IRepository<Staff>
    {
        IEnumerable<Staff> GetStaffsByName(string name);
        StaffDto GetLoginByCredentials(UserLogin login);
        Staff GetStaffByUsername(string username);
    }
}
