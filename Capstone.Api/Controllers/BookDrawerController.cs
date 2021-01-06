using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Capstone.Api.Respones;
using Capstone.Core.CustomEntities;
using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces.BookDrawerInterfaces;
using Capstone.Core.QueryFilters;
using Capstone.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Capstone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookDrawerController : ControllerBase
    {
        private readonly IBookDrawerService _bookDrawerService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        public BookDrawerController(IBookDrawerService bookDrawerService, IMapper mapper, IUriService uriService)
        {
            _bookDrawerService = bookDrawerService;
            _mapper = mapper;
            _uriService = uriService;
        }
        [HttpGet(Name = nameof(GetBookDrawers))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<BookDrawerDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetBookDrawers([FromQuery] BookDrawerQueryFilter filters)
        {
            var bookDrawers = _bookDrawerService.GetBookDrawers(filters);
            var bookDrawerDtos = _mapper.Map<IEnumerable<BookDrawerDto>>(bookDrawers);
            var metadata = new Metadata
            {
                TotalCount = bookDrawers.TotalCount,
                PageSize = bookDrawers.PageSize,
                CurrentPage = bookDrawers.CurrentPage,
                TotalPages = bookDrawers.TotalPages,
                HasNextPage = bookDrawers.HasNextPage,
                HasPreviousPage = bookDrawers.HasPreviousPage,
                NextPageUrl = _uriService.GetBookDrawerPaginationUri(filters, Url.RouteUrl(nameof(GetBookDrawers))).ToString(),
                PreviousPageUrl = _uriService.GetBookDrawerPaginationUri(filters, Url.RouteUrl(nameof(GetBookDrawers))).ToString()
            };

            var response = new ApiResponse<IEnumerable<BookDrawerDto>>(bookDrawerDtos)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _bookDrawerService.GetBookDrawer(id);
            var bookDto = _mapper.Map<BookDto>(book);
            var response = new ApiResponse<BookDto>(bookDto);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Book(BookDrawerDto bookDrawerDto)
        {
            var bookDrawer = _mapper.Map<BookDrawer>(bookDrawerDto);
            await _bookDrawerService.InsertBookDrawer(bookDrawer);
            bookDrawerDto = _mapper.Map<BookDrawerDto>(bookDrawer);
            var response = new ApiResponse<BookDrawerDto>(bookDrawerDto);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(BookDrawerDto bookDrawerDto)
        {
            var bookDrawer = _mapper.Map<BookDrawer>(bookDrawerDto);
            var result = await _bookDrawerService.UpdateBookDrawer(bookDrawer);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int?[] id = null)
        {
            var result = await _bookDrawerService.DeleteBookDrawer(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
