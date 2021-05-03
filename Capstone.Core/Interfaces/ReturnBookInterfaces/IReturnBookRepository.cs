using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.Interfaces
{
    public interface IReturnBookRepository : IRepository<ReturnBook>
    {
        IEnumerable<ReturnBookDto> GetAllReturnBookWithPatronName();
        ReturnBookDto GetReturnById(int id);
        IEnumerable<ReturnBookDto> GetAllReturnGroupByMonth();
    }
}
