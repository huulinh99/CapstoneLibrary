using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.Interfaces
{
    public interface IBorrowBookRepository : IRepository<BorrowBook>
    {
        IEnumerable<BorrowBookDto> GetAllBorrowBookWithPatronName();
        IEnumerable<BorrowBookDto> GetBorrowBookReturnToday();
    }
}
