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
    public class BorrowBookRepository : BaseRepository<BorrowBook>, IBorrowBookRepository
    {
        public BorrowBookRepository(CapstoneContext context) : base(context) { }

        public IEnumerable<BorrowBookDto> GetAllBorrowBookWithCustomerName()
        {
            return _entities.Include(c => c.Customer).Where(x => x.IsDeleted == false).Select(c => new BorrowBookDto
            {
                Id = c.Id,
                StaffId = c.StaffId,
                CustomerId = c.CustomerId,
                CustomerName = c.Customer.Name,
                Image = c.Customer.Image,
                StartTime = c.StartTime,
                EndTime = c.EndTime,
                Quantity = c.BorrowDetail.Where(x => x.BorrowId == c.Id).Count(),
                Total = (float)c.BorrowDetail.Sum(a=>a.Book.BookGroup.Fee) + (float)c.BorrowDetail.Sum(a => a.Book.BookGroup.Fee)
            }).ToList();
        }
    }
}
