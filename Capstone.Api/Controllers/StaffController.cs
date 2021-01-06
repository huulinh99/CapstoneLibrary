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
using Capstone.Core.Enumerations;
using Capstone.Core.Interfaces;
using Capstone.Core.QueryFilters;
using Capstone.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Capstone.Api.Controllers
{
    [Authorize(Roles = nameof(RoleType.admin))]
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public StaffController(IStaffService staffService, IMapper mapper, IUriService uriService)
        {
            _staffService = staffService;
            _mapper = mapper;
            _uriService = uriService;
        }

        [HttpGet(Name = nameof(GetStaffs))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<StaffDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetStaffs([FromQuery] StaffQueryFilter filters)
        {
            var staffs = _staffService.GetStaffs(filters);
            var staffsDto = _mapper.Map<IEnumerable<StaffDto>>(staffs);
            var metadata = new Metadata
            {
                TotalCount = staffs.TotalCount,
                PageSize = staffs.PageSize,
                CurrentPage = staffs.CurrentPage,
                TotalPages = staffs.TotalPages,
                HasNextPage = staffs.HasNextPage,
                HasPreviousPage = staffs.HasPreviousPage,
                NextPageUrl = _uriService.GetStaffPaginationUri(filters, Url.RouteUrl(nameof(GetStaffs))).ToString(),
                PreviousPageUrl = _uriService.GetStaffPaginationUri(filters, Url.RouteUrl(nameof(GetStaffs))).ToString()
            };

            var response = new ApiResponse<IEnumerable<StaffDto>>(staffsDto)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStaff(int id)
        {
            var staff = await _staffService.GetStaff(id);
            var staffDto = _mapper.Map<StaffDto>(staff);
            var response = new ApiResponse<StaffDto>(staffDto);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Staff(StaffDto staffDto)
        {
            var staff = _mapper.Map<Staff>(staffDto);
            await _staffService.InsertStaff(staff);
            staffDto = _mapper.Map<StaffDto>(staff);
            var response = new ApiResponse<StaffDto>(staffDto);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, StaffDto staffDto)
        {
            var staff = _mapper.Map<Staff>(staffDto);
            staff.Id = id;

            var result = await _staffService.UpdateStaff(staff);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery]int?[]id = null)
        {
            var result = await _staffService.DeleteStaff(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
