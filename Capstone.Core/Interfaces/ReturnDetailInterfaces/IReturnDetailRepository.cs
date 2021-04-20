using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.Interfaces
{
    public interface IReturnDetailRepository : IRepository<ReturnDetail>
    {
        ReturnDetailDto GetPatronByBookId(int? bookId);
        IEnumerable<ReturnDetailDto> GetAllReturnDetailWithBookName();
    }
}
