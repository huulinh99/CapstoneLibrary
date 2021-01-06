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
using Capstone.Core.Interfaces;
using Capstone.Core.QueryFilters;
using Capstone.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Capstone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookGroupController : ControllerBase
    {
        private readonly IBookGroupService _bookGroupService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public BookGroupController(IBookGroupService bookGroupService, IMapper mapper, IUriService uriService)
        {
            _bookGroupService = bookGroupService;
            _mapper = mapper;
            _uriService = uriService;
        }

        [HttpGet(Name = nameof(GetBookGroups))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<BookDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetBookGroups([FromQuery] BookGroupQueryFilter filters)
        {
            var bookGroups = _bookGroupService.GetBookGroups(filters);
            var bookGroupsDto = _mapper.Map<IEnumerable<BookGroupDto>>(bookGroups);
            var metadata = new Metadata
            {
                TotalCount = bookGroups.TotalCount,
                PageSize = bookGroups.PageSize,
                CurrentPage = bookGroups.CurrentPage,
                TotalPages = bookGroups.TotalPages,
                HasNextPage = bookGroups.HasNextPage,
                HasPreviousPage = bookGroups.HasPreviousPage,
                NextPageUrl = _uriService.GetBookGroupPaginationUri(filters, Url.RouteUrl(nameof(GetBookGroups))).ToString(),
                PreviousPageUrl = _uriService.GetBookGroupPaginationUri(filters, Url.RouteUrl(nameof(GetBookGroups))).ToString()
            };

            var response = new ApiResponse<IEnumerable<BookGroupDto>>(bookGroupsDto)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookGroup(int id)
        {
            var bookGroup = await _bookGroupService.GetBookGroup(id);
            var bookGroupDto = _mapper.Map<BookGroupDto>(bookGroup);
            var response = new ApiResponse<BookGroupDto>(bookGroupDto);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> BookGroup(BookGroupDto bookGroupDto)
        {
            var bookGroup = _mapper.Map<BookGroup>(bookGroupDto);
            await _bookGroupService.InsertBookGroup(bookGroup);
            bookGroupDto = _mapper.Map<BookGroupDto>(bookGroup);
            var response = new ApiResponse<BookGroupDto>(bookGroupDto);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, BookGroupDto bookGroupDto)
        {
            var bookGroup = _mapper.Map<BookGroup>(bookGroupDto);
            bookGroup.Id = id;

            var result = await _bookGroupService.UpdateBookGroup(bookGroup);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery]int?[]id = null)
        {
            var result = await _bookGroupService.DeleteBookGroup(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    } 
}
