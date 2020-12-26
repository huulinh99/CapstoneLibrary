using Capstone.Core.CustomEntities;
using Capstone.Core.Entities;
using Capstone.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces
{
    public interface IRoleService
    {
        PagedList<Role> GetRoles(RoleQueryFilter filters);
        Task<Role> GetRole(int id);
        Task InsertRole(Role role);
        Task<bool> UpdateRole(Role role);
        Task<bool> DeleteRole(int id);
    }
}
