using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Capstone.Api.Respones;
using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Core.Interfaces.UndefinedErrorInterfaces;
using Capstone.Core.QueryFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Capstone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UndefinedErrorController : ControllerBase
    {
        private readonly IUndefinedErrorService _undefinedErrorService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private static IHttpContextAccessor _httpContextAccessor;

        public UndefinedErrorController(IUndefinedErrorService undefinedErrorService, IMapper mapper, IUriService uriService, IHttpContextAccessor httpContextAccessor)
        {
            _undefinedErrorService = undefinedErrorService;
            _mapper = mapper;
            _uriService = uriService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet(Name = nameof(GetUndefinedErrors))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<UndefinedErrorDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetUndefinedErrors([FromQuery] UndefinedErrorQueryFilter filters)
        {
            //var request = _httpContextAccessor.HttpContext.Request;
            //string str = request.QueryString.ToString();
            //string stringBeforeChar = str.Substring(0, str.IndexOf("&"));
            var undefinedErrors = _undefinedErrorService.GetUndefinedErrors(filters);
            var undefinedErrorsDto = _mapper.Map<IEnumerable<UndefinedErrorDto>>(undefinedErrors);
            //var nextPage = bookGroups.CurrentPage >= 1 && bookGroups.CurrentPage < bookGroups.TotalCount
            //               ? _uriService.GetPageUri(bookGroups.CurrentPage + 1, bookGroups.PageSize, _uriService.GetBookGroupPaginationUri(filters, Url.RouteUrl(nameof(GetBookGroups))).ToString() + stringBeforeChar)
            //               : null;
            //var previousPage = bookGroups.CurrentPage - 1 >= 1 && bookGroups.CurrentPage < bookGroups.TotalCount
            //               ? _uriService.GetPageUri(bookGroups.CurrentPage - 1, bookGroups.PageSize, _uriService.GetBookGroupPaginationUri(filters, Url.RouteUrl(nameof(GetBookGroups))).ToString() + stringBeforeChar)
            //               : null;

            return Ok(undefinedErrorsDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetUndefinedError(int id)
        {
            var undefinedError = _undefinedErrorService.GetUndefinedError(id);
            var undefinedErrorDto = _mapper.Map<UndefinedErrorDto>(undefinedError);
            var response = new ApiResponse<UndefinedErrorDto>(undefinedErrorDto);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult UndefinedError(UndefinedErrorDto undefinedErrorDto)
        {
            var undefinedError = _mapper.Map<UndefinedError>(undefinedErrorDto);
            _undefinedErrorService.InsertUndefinedError(undefinedError);
            undefinedErrorDto = _mapper.Map<UndefinedErrorDto>(undefinedError);
            var response = new ApiResponse<UndefinedErrorDto>(undefinedErrorDto);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Put(int id, UndefinedErrorDto undefinedErrorDto)
        {
            var undefinedError = _mapper.Map<UndefinedError>(undefinedErrorDto);
            undefinedError.Id = id;
            //Debug.WriteLine("run controller");
            var result = _undefinedErrorService.UpdateUndefinedError(undefinedError);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] int?[] id = null)
        {
            var result = _undefinedErrorService.DeleteUndefinedError(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
