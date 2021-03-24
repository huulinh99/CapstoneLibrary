using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces.DetectionErrorInterfaces;
using Capstone.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capstone.Infrastructure.Repositories
{
    public class DetectionErrorRepository : BaseRepository<DetectionError>, IDetectionErrorRepository
    {
        public DetectionErrorRepository(CapstoneContext context) : base(context) { }

        public IEnumerable<DetectionErrorDto> GetAllDetectionError()
        {
            var bookGroup = _entities
                .Select(c => new DetectionErrorDto
                {
                    Id = c.Id,
                    BookId = c.BookId,
                    DrawerDetectionId = c.DrawerDetectionId,
                    ErrorMessage = c.ErrorMessage,
                    BookName = c.Book.BookGroup.Name,
                    IsConfirm = c.IsConfirm,
                    BookBarcode = c.Book.Barcode,
                    TypeError = c.TypeError
                }).ToList();
            return bookGroup;
        }
    }
}
