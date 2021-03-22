using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.Interfaces
{
    public interface IBorrowDetailRepository : IRepository<BorrowDetail>
    {
        IEnumerable<BorrowDetailDto> GetBorrowDetailWithFee(int? borrowId);
        IEnumerable<BorrowDetailDto> GetAllBorrowDetailAndBookName();
        IEnumerable<BorrowDetailDto> GetBorrowDetailWithListBorrow(IEnumerable<BorrowBookDto> borrowBooks);
        BorrowDetailDto GetCustomerByBookId(int? bookId);
    }
}
