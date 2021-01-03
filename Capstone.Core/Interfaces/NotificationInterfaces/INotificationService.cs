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
        Task<Notification> GetNotification(int id);
        Task InsertNotification(Notification notification);
        Task<bool> UpdateNotification(Notification notification);

        Task<bool> DeleteNotification(int[] id);
    }
}
