using Capstone.Core.Entities;
using Capstone.Core.Interfaces.DetectionErrorInterfaces;
using Capstone.Core.Interfaces.DetectionInterfaces;
using Capstone.Core.Interfaces.DrawerDetectionInterfaces;
using Capstone.Core.Interfaces.FavouriteCategoryInterfaces;
using Capstone.Core.Interfaces.ImageInterfaces;
using Capstone.Core.Interfaces.UndefinedErrorInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository BookRepository { get; }
        IBookGroupRepository BookGroupRepository { get; }
        ILocationRepository LocationRepository { get; }
        IBookShelfRepository BookShelfRepository { get; }
        IDrawerRepository DrawerRepository { get; }
        IStaffRepository StaffRepository { get; }
        IBorrowBookRepository BorrowBookRepository { get; }
        IDetectionRepository DetectionRepository { get; }
        IDetectionErrorRepository DetectionErrorRepository { get; }
        IUndefinedErrorRepository UndefinedErrorRepository { get; }
        IDrawerDetectionRepository DrawerDetectionRepository { get; }
        IRoleRepository RoleRepository { get; }
        IBorrowDetailRepository BorrowDetailRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IPatronRepository PatronRepository { get; }
        IReturnBookRepository ReturnBookRepository { get; }
        IReturnDetailRepository ReturnDetailRepository { get; }
        IBookCategoryRepository BookCategoryRepository { get; }
        IFeedbackRepository FeedbackRepository { get; }
        IUserNotificationRepository NotificationRepository { get; }
        IImageRepository ImageRepository { get; }
        IFavouriteCategoryRepository FavouriteCategoryRepository { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}
