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
    public class DrawerController : ControllerBase
    {
        private readonly IDrawerService _drawerService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        public DrawerController(IDrawerService drawerService, IMapper mapper, IUriService uriService)
        {
            _drawerService = drawerService;
            _mapper = mapper;
            _uriService = uriService;
        }
        [HttpGet(Name = nameof(GetDrawers))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<DrawerDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetDrawers([FromQuery] DrawerQueryFilter filters)
        {
            var drawers = _drawerService.GetDrawers(filters);
            var drawersDtos = _mapper.Map<IEnumerable<DrawerDto>>(drawers);
            var metadata = new Metadata
            {
                TotalCount = drawers.TotalCount,
                PageSize = drawers.PageSize,
                CurrentPage = drawers.CurrentPage,
                TotalPages = drawers.TotalPages,
                HasNextPage = drawers.HasNextPage,
                HasPreviousPage = drawers.HasPreviousPage,
                NextPageUrl = _uriService.GetDrawerPaginationUri(filters, Url.RouteUrl(nameof(GetDrawers))).ToString(),
                PreviousPageUrl = _uriService.GetDrawerPaginationUri(filters, Url.RouteUrl(nameof(GetDrawers))).ToString()
            };

            var response = new ApiResponse<IEnumerable<DrawerDto>>(drawersDtos)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDrawer(int id)
        {
            var drawer = await _drawerService.GetDrawer(id);
            var drawerDto = _mapper.Map<DrawerDto>(drawer);
            var response = new ApiResponse<DrawerDto>(drawerDto);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Drawer(DrawerDto drawerDto)
        {
            var post = _mapper.Map<Drawer>(drawerDto);
            await _drawerService.InsertDrawer(post);
            drawerDto = _mapper.Map<DrawerDto>(post);
            var response = new ApiResponse<DrawerDto>(drawerDto);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, DrawerDto drawerDto)
        {
            var drawer = _mapper.Map<Drawer>(drawerDto);
            drawer.Id = id;
            var result = await _drawerService.UpdateDrawer(drawer);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery]int[]id = null)
        {
            var result = await _drawerService.DeleteDrawer(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
