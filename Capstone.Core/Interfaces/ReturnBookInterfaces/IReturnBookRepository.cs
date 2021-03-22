using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.Interfaces
{
    public interface IReturnBookRepository : IRepository<ReturnBook>
    {
        IEnumerable<ReturnBookDto> GetAllReturnBookWithCustomerName();
        IEnumerable<ReturnBookDto> GetAllReturnGroupByMonth();
    }
}
