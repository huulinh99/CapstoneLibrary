using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capstone.Infrastructure.Repositories
{
    public class UserNotificationRepository : BaseRepository<UserNotification>, IUserNotificationRepository
    {
        public UserNotificationRepository(CapstoneContext context) : base(context) { }
        public IEnumerable<UserNotificationDto> GetAllNotification()
        {
            var notification = _entities
                .Select(c => new UserNotificationDto
                {
                    Id = c.Id,
                    BookGroupName = c.BookGroup.Name,
                    CreatedDate = c.CreatedDate,
                    Image = c.BookGroup.Image.FirstOrDefault().Url,
                    Message = c.Message,
                    ReturnDate = c.CreatedDate.Value.AddDays(1).ToString(),
                    Time = c.Time,
                    UserId = c.PatronId
                }).OrderByDescending(x => x.Id).ToList();
            return notification;
        }
    }
}
