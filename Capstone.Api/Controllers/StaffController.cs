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
                HasPreviousPage = staffs.HasPreviousPage
            };

            var response = new ApiResponse<IEnumerable<StaffDto>>(staffsDto)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetStaff(int id)
        {
            var staff = _staffService.GetStaff(id);
            var staffDto = _mapper.Map<StaffDto>(staff);
            var response = new ApiResponse<StaffDto>(staffDto);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Staff(StaffDto staffDto)
        {
            var staff = _mapper.Map<Staff>(staffDto);
            _staffService.InsertStaff(staff);
            staffDto = _mapper.Map<StaffDto>(staff);
            var response = new ApiResponse<StaffDto>(staffDto);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Put(int id, StaffDto staffDto)
        {
            var staff = _mapper.Map<Staff>(staffDto);
            staff.Id = id;
            var result = _staffService.UpdateStaff(staff);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] int?[] id = null)
        {
            var result = _staffService.DeleteStaff(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
