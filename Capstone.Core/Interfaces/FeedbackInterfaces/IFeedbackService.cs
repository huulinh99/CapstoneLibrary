using Capstone.Core.CustomEntities;
using Capstone.Core.Entities;
using Capstone.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces
{
    public interface IFeedbackService
    {
        PagedList<Feedback> GetFeedbacks(FeedbackQueryFilter filters);
        Task<Feedback> GetFeedback(int id);
        Task InsertFeedback(Feedback feedback);
        Task<bool> UpdateFeedback(Feedback feedback);
        Task<bool> DeleteFeedback(int?[] id);
    }
}
