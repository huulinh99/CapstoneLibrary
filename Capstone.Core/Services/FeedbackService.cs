using Capstone.Core.CustomEntities;
using Capstone.Core.DTOs;
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
        public bool DeleteFeedback(int?[] id)
        {
            _unitOfWork.FeedbackRepository.Delete(id);
            _unitOfWork.SaveChanges();
            return true;
        }

        public Feedback GetFeedback(int id)
        {
            return _unitOfWork.FeedbackRepository.GetById(id);
        }

        public PagedList<FeedbackDto> GetFeedbacks(FeedbackQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var feedbacks = _unitOfWork.FeedbackRepository.GetAllFeedback();
            if (filters.BookGroupId != null)
            {
                feedbacks = feedbacks.Where(x => x.BookGroupId == filters.BookGroupId);
            }
            if (filters.PatronId != null)
            {
                feedbacks = feedbacks.Where(x => x.PatronId == filters.PatronId);
            }
            if (filters.Rating != null)
            {
                feedbacks = feedbacks.Where(x => x.Rate == filters.Rating);
            }
            if (filters.ReviewContent != null)
            {
                feedbacks = feedbacks.Where(x => x.ReviewContent == filters.ReviewContent);
            }
            var pagedFeedbacks = PagedList<FeedbackDto>.Create(feedbacks, filters.PageNumber, filters.PageSize);
            return pagedFeedbacks;
        }

         public void InsertFeedback(Feedback feedback)
         {
            _unitOfWork.FeedbackRepository.Add(feedback);
            _unitOfWork.SaveChanges();
        }

        public bool UpdateFeedback(Feedback feedback)
        {
            _unitOfWork.FeedbackRepository.Update(feedback);
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
