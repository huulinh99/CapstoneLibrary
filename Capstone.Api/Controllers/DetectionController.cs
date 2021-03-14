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
using Capstone.Core.Interfaces.DetectionInterfaces;
using Capstone.Core.QueryFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Capstone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetectionController : ControllerBase
    {
        private readonly IDetectionService _detectionService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private static IHttpContextAccessor _httpContextAccessor;

        public DetectionController(IDetectionService detectionService, IMapper mapper, IUriService uriService, IHttpContextAccessor httpContextAccessor)
        {
            _detectionService = detectionService;
            _mapper = mapper;
            _uriService = uriService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet(Name = nameof(GetDetections))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<DetectionDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetDetections([FromQuery] DetectionQueryFilter filters)
        {
            //var request = _httpContextAccessor.HttpContext.Request;
            //string str = request.QueryString.ToString();
            //string stringBeforeChar = str.Substring(0, str.IndexOf("&"));
            var detections = _detectionService.GetDetections(filters);
            var detectionsDto = _mapper.Map<IEnumerable<DetectionDto>>(detections);
            //var nextPage = bookGroups.CurrentPage >= 1 && bookGroups.CurrentPage < bookGroups.TotalCount
            //               ? _uriService.GetPageUri(bookGroups.CurrentPage + 1, bookGroups.PageSize, _uriService.GetBookGroupPaginationUri(filters, Url.RouteUrl(nameof(GetBookGroups))).ToString() + stringBeforeChar)
            //               : null;
            //var previousPage = bookGroups.CurrentPage - 1 >= 1 && bookGroups.CurrentPage < bookGroups.TotalCount
            //               ? _uriService.GetPageUri(bookGroups.CurrentPage - 1, bookGroups.PageSize, _uriService.GetBookGroupPaginationUri(filters, Url.RouteUrl(nameof(GetBookGroups))).ToString() + stringBeforeChar)
            //               : null;
            var metadata = new Metadata
            {
                TotalCount = detections.TotalCount,
                PageSize = detections.PageSize,
                CurrentPage = detections.CurrentPage,
                TotalPages = detections.TotalPages,
                HasNextPage = detections.HasNextPage,
                HasPreviousPage = detections.HasPreviousPage,
            };

            var response = new ApiResponse<IEnumerable<DetectionDto>>(detectionsDto)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetDetection(int id)
        {
            var detection = _detectionService.GetDetection(id);
            var detectionDto = _mapper.Map<DetectionDto>(detection);
            var response = new ApiResponse<DetectionDto>(detectionDto);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Detection(DetectionDto detectionDto)
        {
            var detection = _mapper.Map<Detection>(detectionDto);
            _detectionService.InsertDetection(detection);
            detectionDto = _mapper.Map<DetectionDto>(detection);
            var response = new ApiResponse<DetectionDto>(detectionDto);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Put(int id, DetectionDto detectionDto)
        {
            var detection = _mapper.Map<Detection>(detectionDto);
            detection.Id = id;
            //Debug.WriteLine("run controller");
            var result = _detectionService.UpdateDetection(detection);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] int?[] id = null)
        {
            var result = _detectionService.DeleteDetection(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
