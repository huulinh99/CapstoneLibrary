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
    }
}
