using Capstone.Core.CustomEntities;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Core.QueryFilters;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public FeedbackService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public async Task<bool> DeleteFeedback(int id)
        {
            await _unitOfWork.FeedbackRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<Feedback> GetFeedback(int id)
        {
            return await _unitOfWork.FeedbackRepository.GetById(id);
        }

        public PagedList<Feedback> GetFeedbacks(FeedbackQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var feedbacks = _unitOfWork.FeedbackRepository.GetAll();
            if (filters.BookGroupId != null)
            {
                feedbacks = feedbacks.Where(x => x.BookGroupId == filters.BookGroupId);
            }
            if (filters.CustomerId != null)
            {
                feedbacks = feedbacks.Where(x => x.CustomerId == filters.CustomerId);
            }
            if (filters.Rating != null)
            {
                feedbacks = feedbacks.Where(x => x.Rating == filters.Rating);
            }
            if (filters.ReviewContent != null)
            {
                feedbacks = feedbacks.Where(x => x.ReviewContent == filters.ReviewContent);
            }
            var pagedFeedbacks = PagedList<Feedback>.Create(feedbacks, filters.PageNumber, filters.PageSize);
            return pagedFeedbacks;
        }

         public async Task InsertFeedback(Feedback feedback)
         {
            await _unitOfWork.FeedbackRepository.Add(feedback);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateFeedback(Feedback feedback)
        {
            _unitOfWork.FeedbackRepository.Update(feedback);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
