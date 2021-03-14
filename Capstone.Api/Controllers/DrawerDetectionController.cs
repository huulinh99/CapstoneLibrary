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
using Capstone.Core.Interfaces.DrawerDetectionInterfaces;
using Capstone.Core.QueryFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Capstone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrawerDetectionController : ControllerBase
    {
        private readonly IDrawerDetectionService _drawerDetectionService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private static IHttpContextAccessor _httpContextAccessor;

        public DrawerDetectionController(IDrawerDetectionService drawerDetectionService, IMapper mapper, IUriService uriService, IHttpContextAccessor httpContextAccessor)
        {
            _drawerDetectionService = drawerDetectionService;
            _mapper = mapper;
            _uriService = uriService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet(Name = nameof(GetDrawerDetections))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<DrawerDetectionDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetDrawerDetections([FromQuery] DrawerDetectionQueryFilter filters)
        {
            //var request = _httpContextAccessor.HttpContext.Request;
            //string str = request.QueryString.ToString();
            //string stringBeforeChar = str.Substring(0, str.IndexOf("&"));
            var drawerDetections = _drawerDetectionService.GetDrawerDetections(filters);
            var drawerDetectionsDto = _mapper.Map<IEnumerable<DrawerDetectionDto>>(drawerDetections);
            //var nextPage = bookGroups.CurrentPage >= 1 && bookGroups.CurrentPage < bookGroups.TotalCount
            //               ? _uriService.GetPageUri(bookGroups.CurrentPage + 1, bookGroups.PageSize, _uriService.GetBookGroupPaginationUri(filters, Url.RouteUrl(nameof(GetBookGroups))).ToString() + stringBeforeChar)
            //               : null;
            //var previousPage = bookGroups.CurrentPage - 1 >= 1 && bookGroups.CurrentPage < bookGroups.TotalCount
            //               ? _uriService.GetPageUri(bookGroups.CurrentPage - 1, bookGroups.PageSize, _uriService.GetBookGroupPaginationUri(filters, Url.RouteUrl(nameof(GetBookGroups))).ToString() + stringBeforeChar)
            //               : null;
            var metadata = new Metadata
            {
                TotalCount = drawerDetections.TotalCount,
                PageSize = drawerDetections.PageSize,
                CurrentPage = drawerDetections.CurrentPage,
                TotalPages = drawerDetections.TotalPages,
                HasNextPage = drawerDetections.HasNextPage,
                HasPreviousPage = drawerDetections.HasPreviousPage,
            };

            var response = new ApiResponse<IEnumerable<DrawerDetectionDto>>(drawerDetectionsDto)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetDrawerDetection(int id)
        {
            var drawerDetection = _drawerDetectionService.GetDrawerDetection(id);
            var drawerDetectionDto = _mapper.Map<DrawerDetectionDto>(drawerDetection);
            var response = new ApiResponse<DrawerDetectionDto>(drawerDetectionDto);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult DetectionError(DrawerDetectionDto drawerDetectionDto)
        {
            var drawerDetection = _mapper.Map<DrawerDetection>(drawerDetectionDto);
            _drawerDetectionService.InsertDrawerDetection(drawerDetection);
            drawerDetectionDto = _mapper.Map<DrawerDetectionDto>(drawerDetection);
            var response = new ApiResponse<DrawerDetectionDto>(drawerDetectionDto);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Put(int id, DrawerDetectionDto drawerDetectionDto)
        {
            var drawerDetection = _mapper.Map<DrawerDetection>(drawerDetectionDto);
            drawerDetection.Id = id;
            //Debug.WriteLine("run controller");
            var result = _drawerDetectionService.UpdateDrawerDetection(drawerDetection);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] int?[] id = null)
        {
            var result = _drawerDetectionService.DeleteDrawerDetection(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
