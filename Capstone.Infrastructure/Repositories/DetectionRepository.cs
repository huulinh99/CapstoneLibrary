using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces.DetectionInterfaces;
using Capstone.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capstone.Infrastructure.Repositories
{
    public class DetectionRepository : BaseRepository<Detection>, IDetectionRepository
    {
        public DetectionRepository(CapstoneContext context) : base(context) { }
        public IEnumerable<DetectionDto> GetAllDetection()
        {
            var bookGroup = _entities
                .Where(c => c.IsDeleted == false)
                .Select(c => new DetectionDto
                {
                    Id = c.Id,
                    Thumbnail = c.Thumbnail,
                    Url = c.Url,
                    StaffId = c.StaffId,
                    Time = c.Time,
                    BookShelfName = c.BookShelf.Name,
                    Image = c.Staff.Image,
                    StaffName = c.Staff.Name,
                }).OrderByDescending(c=>c.Id).ToList();
            return bookGroup;
        }
    }
}
