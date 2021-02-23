using AutoMapper;
using Capstone.Api.Respones;
using Capstone.Core.CustomEntities;
using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Core.QueryFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Capstone.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService; 
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private static IHttpContextAccessor _httpContextAccessor;
        public BookController(IBookService bookService, IMapper mapper, IUriService uriService, IHttpContextAccessor httpContextAccessor)
        {
            _bookService = bookService;
            _mapper = mapper;
            _uriService = uriService;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet(Name = nameof(GetBooks))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<BookDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetBooks([FromQuery]BookQueryFilter filters)
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var books = _bookService.GetBooks(filters);
            //string str = request.QueryString.ToString();
            //string stringBeforeChar = str.Substring(0, str.IndexOf("&"));
            var booksDtos = _mapper.Map<IEnumerable<BookDto>>(books);
            //if (stringBeforeChar.Length < 0)
            //{
            //    var nextPage = books.CurrentPage >= 1 && books.CurrentPage < books.TotalCount
            //               ? _uriService.GetPageUri(books.CurrentPage + 1, books.PageSize, _uriService.GetBookPaginationUri(filters, Url.RouteUrl(nameof(GetBooks))).ToString())
            //               : null;
            //    var previousPage = books.CurrentPage - 1 >= 1 && books.CurrentPage < books.TotalCount
            //                   ? _uriService.GetPageUri(books.CurrentPage - 1, books.PageSize, _uriService.GetBookPaginationUri(filters, Url.RouteUrl(nameof(GetBooks))).ToString())
            //                   : null;
            //}
            //else
            //{
            //    var nextPage = books.CurrentPage >= 1 && books.CurrentPage < books.TotalCount
            //               ? _uriService.GetPageUri(books.CurrentPage + 1, books.PageSize, _uriService.GetBookPaginationUri(filters, Url.RouteUrl(nameof(GetBooks))).ToString() + stringBeforeChar)
            //               : null;
            //    var previousPage = books.CurrentPage - 1 >= 1 && books.CurrentPage < books.TotalCount
            //                   ? _uriService.GetPageUri(books.CurrentPage - 1, books.PageSize, _uriService.GetBookPaginationUri(filters, Url.RouteUrl(nameof(GetBooks))).ToString() + stringBeforeChar)
            //                   : null;
            //}
            
            var metadata = new Metadata
            {
                TotalCount = books.TotalCount,
                PageSize = books.PageSize,
                CurrentPage = books.CurrentPage,
                TotalPages = books.TotalPages,
                HasNextPage = books.HasNextPage,
                HasPreviousPage = books.HasPreviousPage
            };

            var response = new ApiResponse<IEnumerable<BookDto>>(booksDtos)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            var book = _bookService.GetBook(id);
            var bookDto = _mapper.Map<BookDto>(book);
            var response = new ApiResponse<BookDto>(bookDto);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Book(BookDto bookDto)
        {
            var post = _mapper.Map<Book>(bookDto);
            _bookService.InsertBook(post);
            bookDto = _mapper.Map<BookDto>(post);
            var response = new ApiResponse<BookDto>(bookDto);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Put(int id, BookDto bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);
            book.Id = id;
            var result = _bookService.UpdateBook(book);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery]int?[]id = null)
        {
            var result = _bookService.DeleteBook(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
