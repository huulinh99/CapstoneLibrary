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
    public interface ICustomerService
    {
        PagedList<Customer> GetCustomers(CustomerQueryFilter filters);
        Customer GetCustomer(int id);
        Customer GetCustomer(string email);
        void InsertCustomer(Customer customer);
        bool UpdateCustomer(Customer customer);
        bool DeleteCustomer(int?[] id);
        CustomerDto GetLoginByCredenticalsCustomer(UserLogin login);
    }
}
