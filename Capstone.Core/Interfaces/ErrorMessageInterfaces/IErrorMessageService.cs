using Capstone.Core.CustomEntities;
using Capstone.Core.Entities;
using Capstone.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces
{
    public interface IErrorMessageService
    {
        PagedList<ErrorMessage> GetErrorMessages(ErrorMessageQueryFilter filters);
        Task<ErrorMessage> GetErrorMessage(int id);
        Task InsertErrorMessage(ErrorMessage errorMessage);
        Task<bool> UpdateErrorMessage(ErrorMessage errorMessage);
        Task<bool> DeleteErrorMessage(int[] id);
    }
}
