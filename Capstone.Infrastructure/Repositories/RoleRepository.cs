using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Infrastructure.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(CapstoneContext context) : base(context) { }
        public IEnumerable<Role> GetRolesByBookName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
