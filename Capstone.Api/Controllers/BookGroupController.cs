using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Capstone.Core.Services;
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
        private static IHttpContextAccessor _httpContextAccessor;

        public BookGroupController(IBookGroupService bookGroupService, IMapper mapper, IUriService uriService, IHttpContextAccessor httpContextAccessor)
        {
            _bookGroupService = bookGroupService;
            _mapper = mapper;
            _uriService = uriService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet(Name = nameof(GetBookGroups))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<BookDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetBookGroups([FromQuery] BookGroupQueryFilter filters)
        {
            //var request = _httpContextAccessor.HttpContext.Request;
            //string str = request.QueryString.ToString();
            //string stringBeforeChar = str.Substring(0, str.IndexOf("&"));
            var bookGroups = _bookGroupService.GetBookGroups(filters);
            var bookGroupsDto = _mapper.Map<IEnumerable<BookGroupDto>>(bookGroups);
            //var nextPage = bookGroups.CurrentPage >= 1 && bookGroups.CurrentPage < bookGroups.TotalCount
            //               ? _uriService.GetPageUri(bookGroups.CurrentPage + 1, bookGroups.PageSize, _uriService.GetBookGroupPaginationUri(filters, Url.RouteUrl(nameof(GetBookGroups))).ToString() + stringBeforeChar)
            //               : null;
            //var previousPage = bookGroups.CurrentPage - 1 >= 1 && bookGroups.CurrentPage < bookGroups.TotalCount
            //               ? _uriService.GetPageUri(bookGroups.CurrentPage - 1, bookGroups.PageSize, _uriService.GetBookGroupPaginationUri(filters, Url.RouteUrl(nameof(GetBookGroups))).ToString() + stringBeforeChar)
            //               : null;
            var metadata = new Metadata
            {
                TotalCount = bookGroups.TotalCount,
                PageSize = bookGroups.PageSize,
                CurrentPage = bookGroups.CurrentPage,
                TotalPages = bookGroups.TotalPages,
                HasNextPage = bookGroups.HasNextPage,
                HasPreviousPage = bookGroups.HasPreviousPage,
            };

            var response = new ApiResponse<IEnumerable<BookGroupDto>>(bookGroupsDto)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetBookGroup(int id)
        {
            var bookGroup = _bookGroupService.GetBookGroup(id);
            var bookGroupDto = _mapper.Map<BookGroupDto>(bookGroup);
            var response = new ApiResponse<BookGroupDto>(bookGroupDto);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult BookGroup(BookGroupDto bookGroupDto)
        {
            var bookGroup = _mapper.Map<BookGroup>(bookGroupDto);          
            _bookGroupService.InsertBookGroup(bookGroup);
            bookGroupDto = _mapper.Map<BookGroupDto>(bookGroup);
            var response = new ApiResponse<BookGroupDto>(bookGroupDto);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Put(int id, BookGroupDto bookGroupDto)
        {
            var bookGroup = _mapper.Map<BookGroup>(bookGroupDto);
            bookGroup.Id = id;
            Debug.WriteLine("run controller");
            var result = _bookGroupService.UpdateBookGroup(bookGroup);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery]int?[]id = null)
        {
            var result = _bookGroupService.DeleteBookGroup(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    } 
}
