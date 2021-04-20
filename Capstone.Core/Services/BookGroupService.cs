using AutoMapper;
using Capstone.Core.CustomEntities;
using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Core.QueryFilters;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Core.Services
{
    public class BookGroupService : IBookGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PaginationOptions _paginationOptions;
        public BookGroupService(IUnitOfWork unitOfWork, IMapper mapper, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
            _mapper = mapper;
        }
        public bool DeleteBookGroup(int?[] id)
        {
            _unitOfWork.BookGroupRepository.Delete(id);
            List<int?> termsList = new List<int?>();
            List<int?> termsListBook = new List<int?>();
            for (int i = 0; i < id.Length; i++)
            {
                var bookCategories = _unitOfWork.BookCategoryRepository.GetBookCategoriesByBookGroup(id[i]);
                var books = _unitOfWork.BookRepository.GetBookByBookGroup(id[i]);
                foreach (var book in books)
                {
                    termsListBook.Add(book.Id);
                }
                foreach (var bookCategory in bookCategories)
                {
                    termsList.Add(bookCategory.Id);
                }
            }
            int?[] bookCateId = termsList.ToArray();
            int?[] bookId = termsListBook.ToArray();
            _unitOfWork.BookCategoryRepository.Delete(bookCateId);
            _unitOfWork.BookRepository.Delete(bookId);
            _unitOfWork.SaveChanges();
            return true;
        }

        public BookGroupDto GetBookGroup(int id)
        {
            var bookCategories = _unitOfWork.BookCategoryRepository.GetBookCategoriesByBookGroup(id);
            var categories = _unitOfWork.CategoryRepository.GetCategoryNameByBookCategory(bookCategories);
            var feedback = _unitOfWork.FeedbackRepository.GetRatingForBookGroup(id);
            return _unitOfWork.BookGroupRepository.GetBookGroupsWithImageById(id, categories, feedback);
        }

        public PagedList<BookGroupDto> GetBookGroups(BookGroupQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var bookGroupDtos = _unitOfWork.BookGroupRepository.GetAllBookGroups();

            var categories = _unitOfWork.CategoryRepository.GetAllCategories();

            var bookCategories = _unitOfWork.BookCategoryRepository.GetAllBookCategoriesByBookGroup();

            var bookGroups = _unitOfWork.BookGroupRepository.GetAllBookGroupsWithCategory(bookGroupDtos, bookCategories, categories);

            if (filters.Name != null)
            {
                bookGroups = bookGroups.Where(x => x.Name.ToLower().Contains(filters.Name.ToLower()));
            }
            if (filters.PatronId != null)
            {
                var favourite = _unitOfWork.FavouriteCategoryRepository.GetFavouriteCategoryForSuggest(filters.PatronId);
                var categoryByCategory = _unitOfWork.BookCategoryRepository.GetBookCategoriesByCategory(favourite.CategoryId);
                bookGroups = _unitOfWork.BookGroupRepository.GetBookGroupsByBookCategory(categoryByCategory);                
            }
            if (filters.Author != null)
            {
                bookGroups = bookGroups.Where(x => x.Author.ToLower().Contains(filters.Author.ToLower()));
            }

            if (filters.IsNewest == true)
            {
                bookGroups = bookGroups.OrderByDescending(x => x.Id).ToList();
            }

            if (filters.IsPopular == true)
            {
                bookGroups = bookGroups.OrderBy(x => x.Id).ToList();
            }

            if (filters.Fee != null)
            {
                bookGroups = bookGroups.Where(x => x.Fee == filters.Fee);
            }

            if (filters.CategoryId != null)
            {
                bookGroups = bookGroups.Where(x => x.Category != null);
                bookGroups = bookGroups.Where(x => x.Category.Any(c=>c.Id == filters.CategoryId));
            }

            if (filters.PunishFee != null)
            {
                bookGroups = bookGroups.Where(x => x.PunishFee == filters.PunishFee);
            }

            var pagedBookGroups = PagedList<BookGroupDto>.Create(bookGroups, filters.PageNumber, filters.PageSize);
            return pagedBookGroups;
        }


        public void InsertBookGroup(BookGroup bookGroup)
        {
            bookGroup.CreatedDate = DateTime.UtcNow;
            _unitOfWork.BookGroupRepository.Add(bookGroup);
            _unitOfWork.SaveChanges();
            foreach (var bookCategory in bookGroup.BookCategory)
            {
                bookCategory.IsDeleted = false;
            }

            foreach (var image in bookGroup.Image)
            {
                image.IsDeleted = false;
            }
            List<Book> listBook = new List<Book>();
            for (int i = 0; i < bookGroup.Quantity; i++)
            {
                var bookModel = new Book()
                {
                    BookGroupId = bookGroup.Id,
                    Barcode = "",
                    IsDeleted = false,
                    IsAvailable = true
                };
                _unitOfWork.BookRepository.Add(bookModel);
                listBook.Add(bookModel);
            }
            _unitOfWork.SaveChanges();
            char[] prefixs = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K',
                'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W','X', 'Y', 'Z' };
            foreach (var bookDto in listBook)
            {
                int div = bookDto.Id / 9999;
                char prefix = prefixs.ElementAt(div);
                string barcodeId = bookDto.Id.ToString();
                string barcode = "";
                barcode += prefix;
                int sum = 0;
                for (int i = 0; i < barcodeId.Length; i++)
                {
                    sum += int.Parse(barcodeId.ElementAt(i).ToString());
                }
                for (int i = 0; i < 4 - barcodeId.Length; i++)
                {
                    barcode += "0";
                }
                barcode += barcodeId.ToString();
                if (sum < 10)
                {
                    barcode += "0";
                }
                barcode += sum.ToString();
                bookDto.BookGroupId = bookGroup.Id;
                bookDto.Barcode = barcode;
                bookDto.IsDeleted = false;
                bookDto.IsAvailable = true;
                var book = _mapper.Map<Book>(bookDto);
                _unitOfWork.BookRepository.Update(book);
                //_unitOfWork.SaveChangesAsync();
            }
            _unitOfWork.SaveChanges();
        }

        public bool UpdateBookGroup(BookGroup bookGroup)
        {
            var images = _unitOfWork.ImageRepository.GetImageByBookGroupId(bookGroup.Id);
            var bookGroupDto = _unitOfWork.BookGroupRepository.GetById(bookGroup.Id);
            var imageId = new List<int>();
            if (bookGroup.Image.Count == 0)
            {
                foreach (var image in images)
                {
                    image.IsDeleted = true;
                    _unitOfWork.ImageRepository.Update(image);
                    _unitOfWork.SaveChangesAsync();
                }
            }
            else
            {
                foreach (var image in bookGroup.Image)
                {
                    if (image.Id == 0)
                    {
                        image.IsDeleted = false;
                        image.BookGroupId = bookGroup.Id;
                        _unitOfWork.ImageRepository.Add(image);
                        _unitOfWork.SaveChanges();
                    }
                    else
                    {
                        imageId.Add(image.Id);
                    }
                }
                var entities = images.Where(f => !imageId.Contains(f.Id)).ToList();
                entities.ForEach(a => a.IsDeleted = true);
            }
            bookGroupDto.Name = bookGroup.Name;
            bookGroupDto.Fee = bookGroup.Fee;
            bookGroupDto.PunishFee = bookGroup.PunishFee;
            bookGroupDto.Quantity = bookGroup.Quantity;
            bookGroupDto.Author = bookGroup.Author;
            bookGroupDto.PublishPlace = bookGroup.PublishPlace;
            bookGroupDto.PublishCompany = bookGroup.PublishCompany;
            bookGroupDto.PublishDate = bookGroup.PublishDate;
            bookGroupDto.Description = bookGroup.Description;
            bookGroupDto.PageNumber = bookGroup.PageNumber;
            bookGroupDto.Height = bookGroup.Height;
            bookGroupDto.Width = bookGroup.Width;
            bookGroupDto.Thick = bookGroup.Thick;
            bookGroupDto.Edition = bookGroup.Edition;
            bookGroupDto.IsDeleted = false;
            _unitOfWork.BookGroupRepository.Update(bookGroupDto);
            _unitOfWork.SaveChanges();
            return true;
        }

    }
}
