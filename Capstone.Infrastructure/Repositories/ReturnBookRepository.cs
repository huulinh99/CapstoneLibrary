using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capstone.Infrastructure.Repositories
{
    public class ReturnBookRepository : BaseRepository<ReturnBook>, IReturnBookRepository
    {
        public ReturnBookRepository(CapstoneContext context) : base(context) { }

        public IEnumerable<ReturnBookDto> GetAllReturnBookWithCustomerName()
        {
            return _entities.Include(c => c.Customer).Where(x => x.IsDeleted == false).Select(c => new ReturnBookDto
            {
                Id = c.Id,
                StaffId = c.StaffId,
                CustomerId = c.CustomerId,
                CustomerName = c.Customer.Name,
                Image = c.Customer.Image,
                ReturnTime = c.ReturnTime,
                Fee = (float)c.ReturnDetail.Sum(a => a.Fee),
                StaffName = c.Staff.Name
                //PunishFee = (float)c.ReturnDetail.Sum(a => a.PunishFee)
            }).ToList();
        }

        public IEnumerable<ReturnBookDto> GetAllReturnGroupByMonth()
        {
            return _entities.Where(x => x.IsDeleted == false)
                .Where(x => x.ReturnTime > DateTime.Now.AddMonths(-5) && x.ReturnTime < DateTime.Now)
                .GroupBy(s => new { month = s.ReturnTime.Month, year = s.ReturnTime.Year })
                .Select(x => new ReturnBookDto { ReturnTime = DateTime.Parse(string.Format("{0}-{1}", x.Key.year, x.Key.month)), Fee = (float)x.Sum(s => s.Fee) })
                .ToList();
        }
    }
}
