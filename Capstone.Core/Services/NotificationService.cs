using Capstone.Core.CustomEntities;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Core.QueryFilters;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public NotificationService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public bool DeleteNotification(int?[] id)
        {
            _unitOfWork.NotificationRepository.Delete(id);
            _unitOfWork.SaveChanges();
            return true;
        }

        public Notification GetNotification(int id)
        {
            return _unitOfWork.NotificationRepository.GetById(id);
        }

        public PagedList<Notification> GetNotifications(NotificationQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var notifications = _unitOfWork.NotificationRepository.GetAll();
            if (filters.UserId != null)
            {
                notifications = notifications.Where(x => x.UserId == filters.UserId);
            }
            if (filters.Time != null)
            {
                notifications = notifications.Where(x => x.Time == filters.Time);
            }
            var pagedNotifications = PagedList<Notification>.Create(notifications, filters.PageNumber, filters.PageSize);
            return pagedNotifications;
        }

        public void InsertNotification(Notification notification)
        {
            _unitOfWork.NotificationRepository.Add(notification);
            _unitOfWork.SaveChangesAsync();
        }

        public bool UpdateNotification(Notification notification)
        {
            _unitOfWork.NotificationRepository.Update(notification);
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
