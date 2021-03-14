using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capstone.Infrastructure.Repositories
{
    public class ReturnDetailRepository : BaseRepository<ReturnDetail>, IReturnDetailRepository
    {
        public ReturnDetailRepository(CapstoneContext context) : base(context) { }

        public ReturnDetailDto GetCustomerByBookId(int? bookId)
        {
            var entities = _entities.Where(c => c.BookId == bookId).OrderBy(c=>c.Id).Select(c => new ReturnDetailDto
            {
                Id = c.Id,
                ReturnId = c.ReturnId,
                BookId = c.BookId,
                CustomerId = c.Return.CustomerId
            }).LastOrDefault();
            return entities;
        }
    }
}
