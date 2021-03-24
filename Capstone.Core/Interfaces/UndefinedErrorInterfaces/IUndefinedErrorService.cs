using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.Interfaces.UndefinedErrorInterfaces
{
    public interface IUndefinedErrorService
    {
        IEnumerable<UndefinedErrorDto> GetUndefinedErrors(UndefinedErrorQueryFilter filters);
        UndefinedError GetUndefinedError(int id);
        void InsertUndefinedError(UndefinedError undefinedError);
        bool UpdateUndefinedError(UndefinedError undefinedError);
        bool DeleteUndefinedError(int?[] id);
    }
}
