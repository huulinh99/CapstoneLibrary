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
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        public LocationController(ILocationService locationService, IMapper mapper, IUriService uriService)
        {
            _locationService = locationService;
            _mapper = mapper;
            _uriService = uriService;
        }
        [HttpGet(Name = nameof(GetLocations))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<LocationDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetLocations([FromQuery] LocationQueryFilter filters)
        {
            var locations = _locationService.GetLocations(filters);
            var locationsDtos = _mapper.Map<IEnumerable<LocationDto>>(locations);
            var metadata = new Metadata
            {
                TotalCount = locations.TotalCount,
                PageSize = locations.PageSize,
                CurrentPage = locations.CurrentPage,
                TotalPages = locations.TotalPages,
                HasNextPage = locations.HasNextPage,
                HasPreviousPage = locations.HasPreviousPage,
                NextPageUrl = _uriService.GetLocationPaginationUri(filters, Url.RouteUrl(nameof(GetLocations))).ToString(),
                PreviousPageUrl = _uriService.GetLocationPaginationUri(filters, Url.RouteUrl(nameof(GetLocations))).ToString()
            };

            var response = new ApiResponse<IEnumerable<LocationDto>>(locationsDtos)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocation(int id)
        {
            var location = await _locationService.GetLocation(id);
            var locationDto = _mapper.Map<LocationDto>(location);
            var response = new ApiResponse<LocationDto>(locationDto);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Location(LocationDto locationDto)
        {
            var post = _mapper.Map<Location>(locationDto);
            await _locationService.InsertLocation(post);
            locationDto = _mapper.Map<LocationDto>(post);
            var response = new ApiResponse<LocationDto>(locationDto);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, LocationDto locationDto)
        {
            var location = _mapper.Map<Location>(locationDto);
            location.Id = id;

            var result = await _locationService.UpdateLocation(location);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery]int?[]id = null)
        {
            var result = await _locationService.DeleteLocation(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
