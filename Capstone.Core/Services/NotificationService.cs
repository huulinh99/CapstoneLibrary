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
        public async Task<bool> DeleteNotification(int id)
        {
            await _unitOfWork.NotificationRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<Notification> GetNotification(int id)
        {
            return await _unitOfWork.NotificationRepository.GetById(id);
        }

        public PagedList<Notification> GetNotifications(NotificationQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var notifications = _unitOfWork.NotificationRepository.GetAll();
            if (filters.CustomerId != null)
            {
                notifications = notifications.Where(x => x.CustomerId == filters.CustomerId);
            }
            if (filters.Message != null)
            {
                notifications = notifications.Where(x => x.Message == filters.Message);
            }
            if (filters.Time != null)
            {
                notifications = notifications.Where(x => x.Time == filters.Time);
            }
            var pagedNotifications = PagedList<Notification>.Create(notifications, filters.PageNumber, filters.PageSize);
            return pagedNotifications;
        }

        public async Task InsertNotification(Notification notification)
        {
            await _unitOfWork.NotificationRepository.Add(notification);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateNotification(Notification notification)
        {
            _unitOfWork.NotificationRepository.Update(notification);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
