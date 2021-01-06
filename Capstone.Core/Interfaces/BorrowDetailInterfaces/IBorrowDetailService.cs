using Capstone.Core.CustomEntities;
using Capstone.Core.Entities;
using Capstone.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces
{
    public interface IBorrowDetailService
    {
        PagedList<BorrowDetail> GetBorrowDetails(BorrowDetailQueryFilter filters);
        Task<BorrowDetail> GetBorrowDetail(int id);
        Task InsertBorrowDetail(BorrowDetail borrowDetail);
        Task<bool> UpdateBorrowDetail(BorrowDetail borrowDetail);

        Task<bool> DeleteBorrowDetail(int?[] id);
    }
}
