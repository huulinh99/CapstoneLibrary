using Capstone.Core.CustomEntities;
using Capstone.Core.Entities;
using Capstone.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces
{
    public interface IReturnBookService
    {
        PagedList<ReturnBook> GetReturnBooks(ReturnBookQueryFilter filters);
        Task<ReturnBook> GetReturnBook(int id);
        Task InsertReturnBook(ReturnBook returnBook);
        Task<bool> UpdateReturnBook(ReturnBook returnBook);

        Task<bool> DeleteReturnBook(int id);
    }
}
