using Capstone.Core.CustomEntities;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Core.Interfaces.ImageInterfaces;
using Capstone.Core.QueryFilters;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Services
{
    public class ImageService : IImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public ImageService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public bool DeleteImage(int?[] id)
        {
            _unitOfWork.ImageRepository.Delete(id);
            _unitOfWork.SaveChanges();
            return true;
        }

        public Image GetImage(int id)
        {
            return _unitOfWork.ImageRepository.GetById(id);
        }

        public PagedList<Image> GetImages(ImageQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var images = _unitOfWork.ImageRepository.GetAll();
            if (filters.BookGroupId != null)
            {
                images = images.Where(x => x.BookGroupId == filters.BookGroupId);
            }
            var pagedImages = PagedList<Image>.Create(images, filters.PageNumber, filters.PageSize);
            return pagedImages;
        }

        public void InsertImage(Image image)
        {
            _unitOfWork.ImageRepository.Add(image);
            _unitOfWork.SaveChanges();
        }

        public bool UpdateImage(Image image)
        {
            _unitOfWork.ImageRepository.Update(image);
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
