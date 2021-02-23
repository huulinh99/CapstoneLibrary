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
        ErrorMessage GetErrorMessage(int id);
        void InsertErrorMessage(ErrorMessage errorMessage);
        bool UpdateErrorMessage(ErrorMessage errorMessage);
        bool DeleteErrorMessage(int?[] id);
    }
}
