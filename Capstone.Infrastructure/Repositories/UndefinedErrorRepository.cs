using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces.UndefinedErrorInterfaces;
using Capstone.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capstone.Infrastructure.Repositories
{
    public class UndefinedErrorRepository : BaseRepository<UndefinedError>, IUndefinedErrorRepository
    {
        public UndefinedErrorRepository(CapstoneContext context) : base(context) { }

        public IEnumerable<UndefinedErrorDto> GetAllUndefinedError()
        {
            var undefinedError = _entities
                .Select(c => new UndefinedErrorDto
                {
                    Id = c.Id,
                    DrawerDetectionId = c.DrawerDetectionId,
                    ErrorMessage = c.ErrorMessage,
                    IsConfirm = c.IsConfirm,
                    IsRejected = c.IsRejected,
                    TypeError = c.TypeError
                }).ToList();
            return undefinedError;
        }
    }
}
