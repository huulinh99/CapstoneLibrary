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
        Task<IEnumerable<Staff>> GetStaffsByName(string name);
        Task<StaffDto> GetLoginByCredentials(UserLogin login);
        Task<Staff> GetStaffByUsername(string username);
    }
}
