using Capstone.Core.CustomEntities;
using Capstone.Core.DTOs;
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
        PagedList<BorrowDetailDto> GetBorrowDetails(BorrowDetailQueryFilter filters);
        BorrowDetail GetBorrowDetail(int id);
        void InsertBorrowDetail(BorrowDetail borrowDetail);
        bool UpdateBorrowDetail(BorrowDetail borrowDetail);

        bool DeleteBorrowDetail(int?[] id);
    }
}
