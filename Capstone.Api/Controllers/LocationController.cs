using AutoMapper;
using Capstone.Api.Respones;
using Capstone.Core.CustomEntities;
using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Core.QueryFilters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
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
                HasPreviousPage = locations.HasPreviousPage
            };

            var response = new ApiResponse<IEnumerable<LocationDto>>(locationsDtos)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetLocation(int id)
        {
            var location =  _locationService.GetLocation(id);
            var locationDto = _mapper.Map<LocationDto>(location);
            var response = new ApiResponse<LocationDto>(locationDto);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Location(LocationDto locationDto)
        {
            var post = _mapper.Map<Location>(locationDto);
            _locationService.InsertLocation(post);
            locationDto = _mapper.Map<LocationDto>(post);
            var response = new ApiResponse<LocationDto>(locationDto);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Put(int id, LocationDto locationDto)
        {
            var location = _mapper.Map<Location>(locationDto);
            location.Id = id;

            var result = _locationService.UpdateLocation(location);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] int?[] id = null)
        {
            var result = _locationService.DeleteLocation(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
