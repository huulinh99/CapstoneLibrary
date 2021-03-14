using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        //Task<Customer> GetCustomerById(int? id);
        Customer GetCustomerByEmail(string email);
        CustomerDto GetLoginByCredentials(UserLogin login);
    }
}
