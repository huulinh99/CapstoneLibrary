using Capstone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces
{
    public interface IErrorMessageRepository : IRepository<ErrorMessage>
    {
        IEnumerable<ErrorMessage> GetErrorMessagesByBookDetectError(int bookDetectErrorId);
    }
}
