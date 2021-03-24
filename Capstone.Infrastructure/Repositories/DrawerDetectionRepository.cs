using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces.DrawerDetectionInterfaces;
using Capstone.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capstone.Infrastructure.Repositories
{
    public class DrawerDetectionRepository : BaseRepository<DrawerDetection>, IDrawerDetectionRepository
    {
        public DrawerDetectionRepository(CapstoneContext context) : base(context) { }
        public IEnumerable<DrawerDetectionDto> GetAllDrawerDetection()
        {
            var bookGroup = _entities
                .Select(c => new DrawerDetectionDto
                {
                    Id = c.Id,
                    //DetectionError = c.DetectionError,
                    DetectionId = c.DetectionId,
                    Count = c.UndefinedError.Count + c.DetectionError.Count,
                    BookShelfName = c.Drawer.BookShelf.Name,
                    DrawerId = c.DrawerId,
                    DrawerBarcode = c.Drawer.Barcode
                }).ToList();
            return bookGroup;
        }
    }
}
