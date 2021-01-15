using Capstone.Core.CustomEntities;
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
        Task<Customer> GetCustomer(int id);
        Task<Customer> GetCustomer(string email);
        Task InsertCustomer(Customer customer);
        Task<bool> UpdateCustomer(Customer customer);
        Task<bool> DeleteCustomer(int?[] id);
    }
}
