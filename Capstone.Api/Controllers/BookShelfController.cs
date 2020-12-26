using AutoMapper;
using Capstone.Api.Respones;
using Capstone.Core.CustomEntities;
using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Core.QueryFilters;
using Capstone.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Capstone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookShelfController : ControllerBase
    {
        private readonly IBookShelfService _bookShelfService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        public BookShelfController(IBookShelfService bookShelfService, IMapper mapper, IUriService uriService)
        {
            _bookShelfService = bookShelfService;
            _mapper = mapper;
            _uriService = uriService;
        }
        [HttpGet(Name = nameof(GetBookShelves))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<BookShelfDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetBookShelves([FromQuery] BookShelfQueryFilter filters)
        {
            var bookShelves = _bookShelfService.GetBookShelves(filters);
            var bookShelvesDtos = _mapper.Map<IEnumerable<BookShelfDto>>(bookShelves);
            var metadata = new Metadata
            {
                TotalCount = bookShelves.TotalCount,
                PageSize = bookShelves.PageSize,
                CurrentPage = bookShelves.CurrentPage,
                TotalPages = bookShelves.TotalPages,
                HasNextPage = bookShelves.HasNextPage,
                HasPreviousPage = bookShelves.HasPreviousPage,
                NextPageUrl = _uriService.GetBookShelfPaginationUri(filters, Url.RouteUrl(nameof(GetBookShelves))).ToString(),
                PreviousPageUrl = _uriService.GetBookShelfPaginationUri(filters, Url.RouteUrl(nameof(GetBookShelves))).ToString()
            };

            var response = new ApiResponse<IEnumerable<BookShelfDto>>(bookShelvesDtos)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookShelf(int id)
        {
            var bookShelf = await _bookShelfService.GetBookShelf(id);
            var bookShelfDto = _mapper.Map<BookShelfDto>(bookShelf);
            var response = new ApiResponse<BookShelfDto>(bookShelfDto);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> BookShelf(BookShelfDto bookShelfDto)
        {
            var post = _mapper.Map<BookShelf>(bookShelfDto);
            await _bookShelfService.InsertBookShelf(post);
            bookShelfDto = _mapper.Map<BookShelfDto>(post);
            var response = new ApiResponse<BookShelfDto>(bookShelfDto);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, BookShelfDto bookShelfDto)
        {
            var bookShelf = _mapper.Map<BookShelf>(bookShelfDto);
            bookShelf.Id = id;

            var result = await _bookShelfService.UpdateBookShelf(bookShelf);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _bookShelfService.DeleteBookShelf(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
