using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NCrontab;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.Core.Services
{
    public class MyTestHostedService : BackgroundService
    {
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        private readonly IServiceProvider _services;

        private string Schedule => "0 15 15 * * *"; //Runs every 10 seconds

        public MyTestHostedService(IServiceProvider services)
        {
            _schedule = CrontabSchedule.Parse(Schedule, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                var now = DateTime.Now;
                var nextrun = _schedule.GetNextOccurrence(now);
                if (now > _nextRun)
                {
                    Process();
                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                }
                await Task.Delay(5000, stoppingToken); //5 seconds delay
            }
            while (!stoppingToken.IsCancellationRequested);
        }

        private async Task Process()
        {
            Console.WriteLine("hello world" + DateTime.Now.ToString("F"));
            
            using (var scope = _services.CreateScope())
            {
                var repo = scope.ServiceProvider
                                      .GetRequiredService<IBorrowBookService>();

                var botService = scope.ServiceProvider
                                      .GetRequiredService<IUserNotificationService>();
                var bookService = scope.ServiceProvider.GetRequiredService<IBookService>();



                var returnToday = repo.GetReturnToday();
                foreach (var today in returnToday)
                {
                    foreach (var borrowDetail in today.BorrowDetail)
                    {
                        var book = bookService.GetBook(borrowDetail.BookId);
                        UserNotification notification = new UserNotification
                        {
                            BookGroupId = book.BookGroupId,
                            CreatedDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")),
                            PatronId = today.PatronId,
                            Time = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")),
                            Message = ""
                        };
                        await botService.InsertNotification(notification);
                    }
                    
                }
            }
        }
    }
}
