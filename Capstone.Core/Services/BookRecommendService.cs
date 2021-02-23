﻿using Capstone.Core.CustomEntities;
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
    public class BookRecommendService : IBookRecommendService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public BookRecommendService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public bool DeleteBookRecommend(int?[] id)
        {
            _unitOfWork.BookDetectRepository.Delete(id);
            _unitOfWork.SaveChanges();
            return true;
        }

        public PagedList<BookRecommend> GetBookRecommend(BookRecommendQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var bookRecommends = _unitOfWork.BookRecommendRepository.GetAll();
            if (filters.BookId != null)
            {
                bookRecommends = bookRecommends.Where(x => x.BookId == filters.BookId);
            }

            if (filters.CampaignId != null)
            {
                bookRecommends = bookRecommends.Where(x => x.CampaignId == filters.CampaignId);
            }


            var pagedBookRecommends = PagedList<BookRecommend>.Create(bookRecommends, filters.PageNumber, filters.PageSize);
            return pagedBookRecommends;
        }

        public BookRecommend GetBookRecommend(int id)
        {
            return _unitOfWork.BookRecommendRepository.GetById(id);
        }

        public void InsertBookRecommend(BookRecommend bookRecommend)
        {
            _unitOfWork.BookRecommendRepository.Add(bookRecommend);
            _unitOfWork.SaveChanges();
        }

        public bool UpdateBookRecommend(BookRecommend bookRecommend)
        {
            _unitOfWork.BookRecommendRepository.Update(bookRecommend);
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
