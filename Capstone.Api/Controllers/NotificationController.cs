using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Capstone.Api.Respones;
using Capstone.Core.CustomEntities;
using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Core.QueryFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Capstone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public NotificationController(INotificationService notificationService, IMapper mapper, IUriService uriService)
        {
            _notificationService = notificationService;
            _mapper = mapper;
            _uriService = uriService;
        }

        [HttpGet(Name = nameof(GetNotifications))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<NotificationDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetNotifications([FromQuery] NotificationQueryFilter filters)
        {
            var notifications = _notificationService.GetNotifications(filters);
            var notificationsDto = _mapper.Map<IEnumerable<NotificationDto>>(notifications);
            var metadata = new Metadata
            {
                TotalCount = notifications.TotalCount,
                PageSize = notifications.PageSize,
                CurrentPage = notifications.CurrentPage,
                TotalPages = notifications.TotalPages,
                HasNextPage = notifications.HasNextPage,
                HasPreviousPage = notifications.HasPreviousPage,
            };

            var response = new ApiResponse<IEnumerable<NotificationDto>>(notificationsDto)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetNotification(int id)
        {
            var notification = _notificationService.GetNotification(id);
            var notificationDto = _mapper.Map<NotificationDto>(notification);
            var response = new ApiResponse<NotificationDto>(notificationDto);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Notification(NotificationDto notificationDto)
        {
            var notification = _mapper.Map<Notification>(notificationDto);
            _notificationService.InsertNotification(notification);
            notificationDto = _mapper.Map<NotificationDto>(notification);
            var response = new ApiResponse<NotificationDto>(notificationDto);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Put(int id, NotificationDto notificationDto)
        {
            var notification = _mapper.Map<Notification>(notificationDto);
            notification.Id = id;

            var result = _notificationService.UpdateNotification(notification);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] int?[] id = null)
        {
            var result = _notificationService.DeleteNotification(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
