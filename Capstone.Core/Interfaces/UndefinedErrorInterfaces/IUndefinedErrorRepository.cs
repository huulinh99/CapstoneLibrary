using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.Interfaces.UndefinedErrorInterfaces
{
    public interface IUndefinedErrorRepository : IRepository<UndefinedError>
    {
        IEnumerable<UndefinedErrorDto> GetAllUndefinedError();
    }
}
