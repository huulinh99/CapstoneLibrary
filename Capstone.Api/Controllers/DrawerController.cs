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
            return Ok(drawersDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetDrawer(int id)
        {
            var drawer =  _drawerService.GetDrawer(id);
            var drawerDto = _mapper.Map<DrawerDto>(drawer);
            var response = new ApiResponse<DrawerDto>(drawerDto);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Drawer(DrawerDto drawerDto)
        {
            var post = _mapper.Map<Drawer>(drawerDto);
            _drawerService.InsertDrawer(post);
            drawerDto = _mapper.Map<DrawerDto>(post);
            var response = new ApiResponse<DrawerDto>(drawerDto);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Put(int id, DrawerDto drawerDto)
        {
            var drawer = _mapper.Map<Drawer>(drawerDto);
            drawer.Id = id;
            var result = _drawerService.UpdateDrawer(drawer);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery]int?[]id = null)
        {
            var result = _drawerService.DeleteDrawer(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
