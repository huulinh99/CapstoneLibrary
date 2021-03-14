using AutoMapper;
using Capstone.Api.Respones;
using Capstone.Core.CustomEntities;
using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Core.QueryFilters;
using Capstone.Core.Services;
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
                NextPageUrl = bookShelves.NextPage,
                PreviousPageUrl = bookShelves.PreviousPage
            };

            var response = new ApiResponse<IEnumerable<BookShelfDto>>(bookShelvesDtos)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetBookShelf(int id)
        {
            var bookShelf = _bookShelfService.GetBookShelf(id);
            var bookShelfDto = _mapper.Map<BookShelfDto>(bookShelf);
            var response = new ApiResponse<BookShelfDto>(bookShelfDto);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult BookShelf(BookShelfDto bookShelfDto)
        {
            var bookShelf = _mapper.Map<BookShelf>(bookShelfDto);
            _bookShelfService.InsertBookShelf(bookShelf);
            bookShelfDto = _mapper.Map<BookShelfDto>(bookShelf);
            var response = new ApiResponse<BookShelfDto>(bookShelfDto);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Put(int id, BookShelfDto bookShelfDto)
        {
            var bookShelf = _mapper.Map<BookShelf>(bookShelfDto);
            bookShelf.Id = id;

            var result = _bookShelfService.UpdateBookShelf(bookShelf);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery]int?[]id = null)
        {
            var result = _bookShelfService.DeleteBookShelf(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
