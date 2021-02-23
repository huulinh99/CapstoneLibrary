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
        Role GetRole(int id);
        void InsertRole(Role role);
        bool UpdateRole(Role role);
        bool DeleteRole(int?[] id);
    }
}
