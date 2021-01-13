using AutoMapper;
using Capstone.Api.Respones;
using Capstone.Core.CustomEntities;
using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Core.QueryFilters;
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
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        public RoleController(IRoleService roleService, IMapper mapper, IUriService uriService)
        {
            _roleService = roleService;
            _mapper = mapper;
            _uriService = uriService;
        }
        [HttpGet(Name = nameof(GetRoles))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<RoleDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetRoles([FromQuery] RoleQueryFilter filters)
        {
            var roles = _roleService.GetRoles(filters);
            var rolesDtos = _mapper.Map<IEnumerable<RoleDto>>(roles);
            var metadata = new Metadata
            {
                TotalCount = roles.TotalCount,
                PageSize = roles.PageSize,
                CurrentPage = roles.CurrentPage,
                TotalPages = roles.TotalPages,
                HasNextPage = roles.HasNextPage,
                HasPreviousPage = roles.HasPreviousPage
            };

            var response = new ApiResponse<IEnumerable<RoleDto>>(rolesDtos)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole(int id)
        {
            var role = await _roleService.GetRole(id);
            var roleDto = _mapper.Map<RoleDto>(role);
            var response = new ApiResponse<RoleDto>(roleDto);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Role(RoleDto roleDto)
        {
            var post = _mapper.Map<Role>(roleDto);
            await _roleService.InsertRole(post);
            roleDto = _mapper.Map<RoleDto>(post);
            var response = new ApiResponse<RoleDto>(roleDto);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, RoleDto roleDto)
        {
            var role = _mapper.Map<Role>(roleDto);
            role.Id = id;

            var result = await _roleService.UpdateRole(role);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int?[] id = null)
        {
            var result = await _roleService.DeleteRole(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
