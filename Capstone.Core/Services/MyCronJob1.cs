using Capstone.Core.CustomEntities;
using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Core.QueryFilters;
using FirebaseAdmin.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.Core.Services
{
    public class MyCronJob1 : CronJobService
    {
        private readonly ILogger<MyCronJob1> _logger;
        private readonly IServiceProvider _services;

        public MyCronJob1(IScheduleConfig<MyCronJob1> config, ILogger<MyCronJob1> logger, IServiceProvider services)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            _logger = logger;
            _services = services;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("CronJob 1 starts.");
            return base.StartAsync(cancellationToken);
        }

        public override Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} CronJob 1 is working.");
            UserNotification notification = new UserNotification
            {
                BookGroupId = 209,
                CreatedDate = DateTime.UtcNow,
                PatronId = 1,
                Time = DateTime.UtcNow,
                Message = "aSDASD"
            };
            using (var scope = _services.CreateScope())
            {
                var botService = scope.ServiceProvider
                                      .GetRequiredService<UserNotificationService>();
                botService.InsertNotification(notification);
            }
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("CronJob 1 is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}
