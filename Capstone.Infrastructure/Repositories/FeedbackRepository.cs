using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Infrastructure.Repositories
{
    public class FeedbackRepository : BaseRepository<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(CapstoneContext context) : base(context) { }

        public ICollection<RatingDto> GetRatingForBookGroup(int? bookGroupId)
        {
            List<RatingDto> list = new List<RatingDto>();
                       
            for (int i = 1; i <= 5; i++)
            {
                var ratingDto = _entities.Where(x => x.BookGroupId == bookGroupId).Select(c => new RatingDto
                {

                    Rating = i,
                    Count = _entities.Count(t => t.Rating == i && t.BookGroupId == bookGroupId)
                }).FirstOrDefault();
                list.Add(ratingDto);
            }         
            return list;
        }

    }
}
