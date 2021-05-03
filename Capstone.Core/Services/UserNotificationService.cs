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

        public PagedList<UserNotificationDto> GetNotifications(NotificationQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var notifications = _unitOfWork.NotificationRepository.GetAllNotification();
            if (filters.PatronId != null)
            {
                notifications = notifications.Where(x => x.UserId == filters.PatronId);
            }
            if (filters.Time != null)
            {
                notifications = notifications.Where(x => x.Time == filters.Time);
            }
            var pagedNotifications = PagedList<UserNotificationDto>.Create(notifications, filters.PageNumber, filters.PageSize);
            return pagedNotifications;
        }

        public async Task InsertNotification(UserNotification notification)
        {
            var patron = _unitOfWork.PatronRepository.GetById(notification.PatronId);
            var bookGroup = _unitOfWork.BookGroupRepository.GetAllBookGroups().Where(x => x.Id == notification.BookGroupId).FirstOrDefault();
            var image = bookGroup.Image.FirstOrDefault().Url;
            notification.Message =  bookGroup.Name;
            var message = new Message()
            {
                Data = new Dictionary<string, string>()
                {
                    ["PatronId"] = notification.PatronId.ToString(),
                    ["CreatedDate"] = notification.CreatedDate.ToString(),
                    ["returnDate"] = notification.Time.ToString(),
                    ["link"] = image,
                    ["click_action"] = "FLUTTER_NOTIFICATION_CLICK"
                },
                Notification = new Notification
                {
                    Title = "Notify about book return",
                    //ImageUrl = "Notify about book return",
                    Body = notification.Message               
                },
                Token = patron.DeviceToken
            };
            _unitOfWork.NotificationRepository.Add(notification);
            _unitOfWork.SaveChanges();
            var messaging = FirebaseMessaging.DefaultInstance;
            var result = await messaging.SendAsync(message);           
        }

        public bool UpdateNotification(UserNotification notification)
        {
            _unitOfWork.NotificationRepository.Update(notification);
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
