using Capstone.Core.CustomEntities;
using Capstone.Core.Entities;
using Capstone.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces
{
    public interface IBorrowBookService
    {
        PagedList<BorrowBook> GetBorrowBooks(BorrowBookQueryFilter filters);
        Task<BorrowBook> GetBorrowBook(int id);
        Task InsertBorrowBook(BorrowBook borrowBook);
        Task<bool> UpdateBorrowBook(BorrowBook borrowBook);
        Task<bool> DeleteBorrowBook(int?[] id);
    }
}
