using Capstone.Core.CustomEntities;
using Capstone.Core.Entities;
using Capstone.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces
{
    public interface INotificationService
    {
        PagedList<Notification> GetNotifications(NotificationQueryFilter filters);
        Notification GetNotification(int id);
        void InsertNotification(Notification notification);
        bool UpdateNotification(Notification notification);

        bool DeleteNotification(int?[] id);
    }
}
