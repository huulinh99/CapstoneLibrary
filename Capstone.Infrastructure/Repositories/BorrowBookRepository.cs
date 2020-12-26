using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Infrastructure.Repositories
{
    public class BorrowBookRepository : BaseRepository<BorrowBook>, IBorrowBookRepository
    {
        public BorrowBookRepository(CapstoneContext context) : base(context) { }
    }
}
