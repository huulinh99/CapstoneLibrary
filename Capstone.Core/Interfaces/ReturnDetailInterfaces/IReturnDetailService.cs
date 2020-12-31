using Capstone.Core.CustomEntities;
using Capstone.Core.Entities;
using Capstone.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces
{
    public interface IReturnDetailService
    {
        PagedList<ReturnDetail> GetReturnDetails(ReturnDetailQueryFilter filters);
        Task<ReturnDetail> GetReturnDetail(int id);
        Task InsertReturnDetail(ReturnDetail returnDetail);
        Task<bool> UpdateReturnDetail(ReturnDetail returnDetail);
        Task<bool> DeleteReturnDetail(int id);
    }
}
