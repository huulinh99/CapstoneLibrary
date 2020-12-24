using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Infrastructure.Repositories
{
    public class ErrorMessageRepository : BaseRepository<ErrorMessage>, IErrorMessageRepository
    {
        public ErrorMessageRepository(CapstoneContext context) : base(context) { }
        public async Task<IEnumerable<ErrorMessage>> GetErrorMessagesByBookDetectError(int bookDetectErrorId)
        {
            return await _entities.Where(x => x.BookDetectErrorId == bookDetectErrorId).ToListAsync();
        }
    }
}
