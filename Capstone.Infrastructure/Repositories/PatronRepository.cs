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
    public class PatronRepository : BaseRepository<Patron>, IPatronRepository
    {
        public PatronRepository(CapstoneContext context) : base(context) { }

        //public IEnumerable<patronDto> GetAllpatron()
        //{
        //    return _entities.Include(c => c.ReturnBook).Where(x => x.IsDeleted == false).Select(c => new patronDto
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
        public Patron GetPatronByEmail(string email)
        {
            return _entities.Where(x => x.Email == email && x.IsDeleted == false).FirstOrDefault();
        }

        public PatronDto GetLoginByCredentials(UserLogin login)
        {
            return _entities.Where(x => x.Username == login.Username
            && x.Password == login.Password
            && x.IsDeleted == false)
                .Include(c => c.Role)
                .Select(c => new PatronDto
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
