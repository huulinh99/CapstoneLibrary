using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.Interfaces
{
    public interface IUserNotificationRepository : IRepository<UserNotification>
    {
        IEnumerable<UserNotificationDto> GetAllNotification();
    }
}
