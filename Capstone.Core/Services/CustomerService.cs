using Capstone.Core.CustomEntities;
using Capstone.Core.DTOs;
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
        public bool DeleteCustomer(int?[] id)
        {
            _unitOfWork.CustomerRepository.Delete(id);
            _unitOfWork.SaveChanges();
            return true;
        }

        public CustomerDto GetLoginByCredenticalsCustomer(UserLogin login)
        {
            return _unitOfWork.CustomerRepository.GetLoginByCredentials(login);
        }
        public Customer GetCustomer(int id)
        {
            return _unitOfWork.CustomerRepository.GetById(id);
        }

        public Customer GetCustomer(string email)
        {
            return _unitOfWork.CustomerRepository.GetCustomerByEmail(email);
        }
        public PagedList<Customer> GetCustomers(CustomerQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var customers = _unitOfWork.CustomerRepository.GetAll();
            if (filters.Name != null)
            {
                customers = customers.Where(x => x.Name.ToLower().Contains(filters.Name.ToLower()));
            }

            if(filters.IsNewest == true)
            {
                customers = customers.OrderByDescending(x => x.Id).Take(5);
            }

            if (filters.Email != null)
            {
                customers = customers.Where(x => x.Email == filters.Email);
            }
            var pagedCustomers = PagedList<Customer>.Create(customers, filters.PageNumber, filters.PageSize);
            return pagedCustomers;
        }

        public void InsertCustomer(Customer customer)
        {
            customer.IsDeleted = false;
            customer.RoleId = 2;
            _unitOfWork.CustomerRepository.Add(customer);
            _unitOfWork.SaveChanges();
        }

        public bool UpdateCustomer(Customer customer)
        {
            _unitOfWork.CustomerRepository.Update(customer);
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
