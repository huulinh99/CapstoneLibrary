using AutoMapper;
using Capstone.Api.Respones;
using Capstone.Core.DTOs;
using Capstone.Core.Interfaces;
using Capstone.Core.QueryFilters;
using Microsoft.AspNetCore.Mvc;
using Capstone.Core.CustomEntities;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Capstone.Infrastructure.Services;
using Capstone.Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Capstone.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        public BookController(IBookService bookService, IMapper mapper, IUriService uriService)
        {
            _bookService = bookService;
            _mapper = mapper;
            _uriService = uriService;
        }
        [HttpGet(Name = nameof(GetBooks))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<BookDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetBooks([FromQuery]BookQueryFilter filters)
        {
            var books = _bookService.GetBooks(filters);
            var booksDtos = _mapper.Map<IEnumerable<BookDto>>(books);
            var metadata = new Metadata
            {
                TotalCount = books.TotalCount,
                PageSize = books.PageSize,
                CurrentPage = books.CurrentPage,
                TotalPages = books.TotalPages,
                HasNextPage = books.HasNextPage,
                HasPreviousPage = books.HasPreviousPage,
                NextPageUrl = _uriService.GetBookPaginationUri(filters, Url.RouteUrl(nameof(GetBooks))).ToString(),
                PreviousPageUrl = _uriService.GetBookPaginationUri(filters, Url.RouteUrl(nameof(GetBooks))).ToString()
            };

            var response = new ApiResponse<IEnumerable<BookDto>>(booksDtos)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _bookService.GetBook(id);
            var bookDto = _mapper.Map<BookDto>(book);
            var response = new ApiResponse<BookDto>(bookDto);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Book(BookDto bookDto)
        {
            var post = _mapper.Map<Book>(bookDto);
            await _bookService.InsertBook(post);
            bookDto = _mapper.Map<BookDto>(post);
            var response = new ApiResponse<BookDto>(bookDto);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, BookDto bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);
            book.Id = id;

            var result = await _bookService.UpdateBook(book);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _bookService.DeleteBook(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
