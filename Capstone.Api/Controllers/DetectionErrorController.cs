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
using Capstone.Core.Interfaces.DetectionErrorInterfaces;
using Capstone.Core.QueryFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Capstone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetectionErrorController : ControllerBase
    {
        private readonly IDetectionErrorService _detectionErrorService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private static IHttpContextAccessor _httpContextAccessor;

        public DetectionErrorController(IDetectionErrorService detectionErrorService, IMapper mapper, IUriService uriService, IHttpContextAccessor httpContextAccessor)
        {
            _detectionErrorService = detectionErrorService;
            _mapper = mapper;
            _uriService = uriService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet(Name = nameof(GetDetectionErrors))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<DetectionErrorDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetDetectionErrors([FromQuery] DetectionErrorQueryFilter filters)
        {
            //var request = _httpContextAccessor.HttpContext.Request;
            //string str = request.QueryString.ToString();
            //string stringBeforeChar = str.Substring(0, str.IndexOf("&"));
            var detectionErrors = _detectionErrorService.GetDetectionErrors(filters);
            var detectionErrorsDto = _mapper.Map<IEnumerable<DetectionErrorDto>>(detectionErrors);
            //var nextPage = bookGroups.CurrentPage >= 1 && bookGroups.CurrentPage < bookGroups.TotalCount
            //               ? _uriService.GetPageUri(bookGroups.CurrentPage + 1, bookGroups.PageSize, _uriService.GetBookGroupPaginationUri(filters, Url.RouteUrl(nameof(GetBookGroups))).ToString() + stringBeforeChar)
            //               : null;
            //var previousPage = bookGroups.CurrentPage - 1 >= 1 && bookGroups.CurrentPage < bookGroups.TotalCount
            //               ? _uriService.GetPageUri(bookGroups.CurrentPage - 1, bookGroups.PageSize, _uriService.GetBookGroupPaginationUri(filters, Url.RouteUrl(nameof(GetBookGroups))).ToString() + stringBeforeChar)
            //               : null;

            return Ok(detectionErrors);
        }

        [HttpGet("{id}")]
        public IActionResult GetDetectionError(int id)
        {
            var detectionError = _detectionErrorService.GetDetectionError(id);
            var detectionErrorDto = _mapper.Map<DetectionErrorDto>(detectionError);
            var response = new ApiResponse<DetectionErrorDto>(detectionErrorDto);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult DetectionError(DetectionErrorDto detectionErrorDto)
        {
            var detectionError = _mapper.Map<DetectionError>(detectionErrorDto);
            _detectionErrorService.InsertDetectionError(detectionError);
            detectionErrorDto = _mapper.Map<DetectionErrorDto>(detectionError);
            var response = new ApiResponse<DetectionErrorDto>(detectionErrorDto);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Put(int id, DetectionErrorDto detectionErrorDto)
        {
            var detectionError = _mapper.Map<DetectionError>(detectionErrorDto);
            detectionError.Id = id;
            //Debug.WriteLine("run controller");
            var result = _detectionErrorService.UpdateDetectionError(detectionError);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] int?[] id = null)
        {
            var result = _detectionErrorService.DeleteDetectionError(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
