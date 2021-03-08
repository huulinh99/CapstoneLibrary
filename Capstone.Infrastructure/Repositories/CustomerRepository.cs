using Capstone.Core.DTOs;
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

        //public IEnumerable<CustomerDto> GetAllCustomer()
        //{
        //    return _entities.Include(c => c.ReturnBook).Where(x => x.IsDeleted == false).Select(c => new CustomerDto
        //    {
        //        Id = c.Id,
        //        Name = c.Name,
        //        Email = c.Email,
        //        Address = c.Address,
        //        CreatedTime = c.CreatedTime,
        //        DoB = c.DoB,
        //        Gender = c.Gender,
        //        Phone = c.Phone,
        //        TotalFee = c.ReturnBook.Fee
        //        BorrowBook = c.ReturnBook.ToList().Count()
        //    }).ToList();
        //}
        public Customer GetCustomerByEmail(string email)
        {
            return _entities.Where(x => x.Email == email && x.IsDeleted == false).FirstOrDefault();
        }

        public CustomerDto GetLoginByCredentials(UserLogin login)
        {
            return _entities.Where(x => x.Username == login.Username
            && x.Password == login.Password
            && x.IsDeleted == false)
                .Include(c => c.Role)
                .Select(c => new CustomerDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Username = c.Username,
                    Password = c.Password,
                    Address = c.Address,
                    DoB = c.DoB,
                    Email = c.Email,
                    Gender = c.Gender,
                    Phone = c.Phone,
                    RoleId = c.Role.Id,
                    Image = c.Image,
                    CreatedTime = c.CreatedTime,
                    DeviceToken = c.DeviceToken
                }).FirstOrDefault();
        }
    }
}
