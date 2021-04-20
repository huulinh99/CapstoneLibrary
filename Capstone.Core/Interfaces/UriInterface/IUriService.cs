using Capstone.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.Interfaces
{
    public interface IUriService
    {
        Uri GetPageUri(int pageNumber, int pageSize, string actionUrl);
        Uri GetBookPaginationUri(BookQueryFilter filter, string actionUrl);
        Uri GetBookGroupPaginationUri(BookGroupQueryFilter filter, string actionUrl);
        Uri GetLocationPaginationUri(LocationQueryFilter filter, string actionUrl);
        Uri GetBookShelfPaginationUri(BookShelfQueryFilter filter, string actionUrl);
        Uri GetDrawerPaginationUri(DrawerQueryFilter filter, string actionUrl);
        Uri GetErrorMessagePaginationUri(ErrorMessageQueryFilter filter, string actionUrl);
        Uri GetStaffPaginationUri(StaffQueryFilter filter, string actionUrl);
        Uri GetBorrowBookPaginationUri(BorrowBookQueryFilter filter, string actionUrl);
        Uri GetRolePaginationUri(RoleQueryFilter filter, string actionUrl);
        Uri GetBorrowDetailPaginationUri(BorrowDetailQueryFilter filter, string actionUrl);
        Uri GetCategoryPaginationUri(CategoryQueryFilter filter, string actionUrl);
        Uri GetCampaignPaginationUri(CampaignQueryFilter filter, string actionUrl);
        Uri GetPatronPaginationUri(PatronQueryFilter filter, string actionUrl);
        Uri GetReturnBookPaginationUri(ReturnBookQueryFilter filter, string actionUrl);
        Uri GetReturnDetailPaginationUri(ReturnDetailQueryFilter filter, string actionUrl);
        Uri GetDetectionPaginationUri(DetectionQueryFilter filter, string actionUrl);
        Uri GetDetectionErrorPaginationUri(DetectionErrorQueryFilter filter, string actionUrl);
        Uri GetDrawerDetectionPaginationUri(DrawerDetectionQueryFilter filter, string actionUrl);
        Uri GetFeedbackPaginationUri(FeedbackQueryFilter filter, string actionUrl);
        Uri GetBookDrawerPaginationUri(BookDrawerQueryFilter filter, string actionUrl);
        Uri GetFavouriteCategoryPaginationUri(FavouriteCategoryQueryFilter filter, string actionUrl);

    }
}
