using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces
{
    public interface IFeedbackRepository : IRepository<Feedback>
    {
        ICollection<RatingDto> GetRatingForBookGroup(int? bookGroupId);
        IEnumerable<FeedbackDto> GetAllFeedback();
    }
}
