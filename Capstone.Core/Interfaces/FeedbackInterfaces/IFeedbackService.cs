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
        Feedback GetFeedback(int id);
        void InsertFeedback(Feedback feedback);
        bool UpdateFeedback(Feedback feedback);
        bool DeleteFeedback(int?[] id);
    }
}
