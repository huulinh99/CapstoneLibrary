using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Infrastructure.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(CapstoneContext context) : base(context) { }

        public async Task<Customer> GetCustomerById(int? id)
        {
            return await _entities.Where(x => x.Id == id && x.IsDeleted == false).FirstOrDefaultAsync();
        }
    }
}
