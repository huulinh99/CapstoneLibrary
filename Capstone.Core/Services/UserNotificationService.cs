using Capstone.Core.CustomEntities;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Core.QueryFilters;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebaseAdmin.Messaging;
using System.Threading.Tasks;
using Capstone.Core.DTOs;
using System.Diagnostics;

namespace Capstone.Core.Services
{
    public class UserNotificationService : IUserNotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public UserNotificationService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
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

        public UserNotification GetNotification(int id)
        {
            return _unitOfWork.NotificationRepository.GetById(id);
        }

        public PagedList<UserNotification> GetNotifications(NotificationQueryFilter filters)
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
            var pagedNotifications = PagedList<UserNotification>.Create(notifications, filters.PageNumber, filters.PageSize);
            return pagedNotifications;
        }

        public async Task InsertNotification(UserNotification notification)
        {
            var message = new Message()
            {
                Data = new Dictionary<string, string>()
                {
                    ["UserId"] = notification.Id.ToString(),
                    ["CreatedDate"] = notification.CreatedDate.ToString()
                },
                Notification = new Notification
                {
                    Title = notification.Message,
                    Body = notification.Message               
                },
                Topic = "news",
                Token = "cniYIC5OST25Ey81Fya92M:APA91bEdkCqYyNoAn3pZTeWNn1ytyDlcBxFosIw1Iy6Jk7r0YOeO8ahj05jQ_CSeB75J1pcKSzTwOG7FSEHiuPiaBU1XNdJkf2_8v1rW1owVnI3EXGsd8abPMVaJvDG5vT1QREZeGY_T"
            };
            var messaging = FirebaseMessaging.DefaultInstance;
            var result = await messaging.SendAsync(message);
            Debug.WriteLine(result);
        }

        public bool UpdateNotification(UserNotification notification)
        {
            _unitOfWork.NotificationRepository.Update(notification);
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
