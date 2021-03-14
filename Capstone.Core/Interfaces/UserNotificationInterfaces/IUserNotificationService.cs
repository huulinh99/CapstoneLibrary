using Capstone.Core.CustomEntities;
using Capstone.Core.Entities;
using Capstone.Core.QueryFilters;
using FirebaseAdmin.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces
{
    public interface IUserNotificationService
    {
        PagedList<UserNotification> GetNotifications(NotificationQueryFilter filters);
        UserNotification GetNotification(int id);
        Task InsertNotification(UserNotification notification);
        bool UpdateNotification(UserNotification notification);

        bool DeleteNotification(int?[] id);
    }
}
