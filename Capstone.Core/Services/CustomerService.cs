using Capstone.Core.CustomEntities;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Core.QueryFilters;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public CustomerService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public async Task<bool> DeleteCustomer(int?[] id)
        {
            await _unitOfWork.CustomerRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<Customer> GetCustomer(int id)
        {
            return await _unitOfWork.CustomerRepository.GetById(id);
        }

        public async Task<Customer> GetCustomer(string email)
        {
            return await _unitOfWork.CustomerRepository.GetCustomerByEmail(email);
        }
        public PagedList<Customer> GetCustomers(CustomerQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var customers = _unitOfWork.CustomerRepository.GetAll();
            if (filters.Name != null)
            {
                customers = customers.Where(x => x.Name == filters.Name);
            }
            if (filters.Email != null)
            {
                customers = customers.Where(x => x.Email == filters.Email);
            }
            var pagedCustomers = PagedList<Customer>.Create(customers, filters.PageNumber, filters.PageSize);
            return pagedCustomers;
        }

        public async Task InsertCustomer(Customer customer)
        {
            customer.IsDeleted = false;
            customer.RoleId = 2;
            await _unitOfWork.CustomerRepository.Add(customer);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateCustomer(Customer customer)
        {
            _unitOfWork.CustomerRepository.Update(customer);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
