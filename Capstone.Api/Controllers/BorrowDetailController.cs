﻿using AutoMapper;
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
    public class BorrowDetailController : ControllerBase
    {
        private readonly IBorrowDetailService _borrowDetailService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        public BorrowDetailController(IBorrowDetailService borrowDetailService, IMapper mapper, IUriService uriService)
        {
            _borrowDetailService = borrowDetailService;
            _mapper = mapper;
            _uriService = uriService;
            //gg
        }
        [HttpGet(Name = nameof(GetBorrowDetails))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<BorrowDetailDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetBorrowDetails([FromQuery] BorrowDetailQueryFilter filters)
        {
            var borrowDetails = _borrowDetailService.GetBorrowDetails(filters);
            var borrowDetailsDtos = _mapper.Map<IEnumerable<BorrowDetailDto>>(borrowDetails);
            var metadata = new Metadata
            {
                TotalCount = borrowDetails.TotalCount,
                PageSize = borrowDetails.PageSize,
                CurrentPage = borrowDetails.CurrentPage,
                TotalPages = borrowDetails.TotalPages,
                HasNextPage = borrowDetails.HasNextPage,
                HasPreviousPage = borrowDetails.HasPreviousPage
            };

            var response = new ApiResponse<IEnumerable<BorrowDetailDto>>(borrowDetailsDtos)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBorrowDetail(int id)
        {
            var borrowDetail = await _borrowDetailService.GetBorrowDetail(id);
            var borrowDetailDto = _mapper.Map<BorrowDetailDto>(borrowDetail);
            var response = new ApiResponse<BorrowDetailDto>(borrowDetailDto);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> BorrowDetail(BorrowDetailDto borrowDetailDto)
        {
            var borrowDetail = _mapper.Map<BorrowDetail>(borrowDetailDto);
            await _borrowDetailService.InsertBorrowDetail(borrowDetail);
            borrowDetailDto = _mapper.Map<BorrowDetailDto>(borrowDetail);
            var response = new ApiResponse<BorrowDetailDto>(borrowDetailDto);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, BorrowDetailDto borrowDetailDto)
        {
            var borrowDetail = _mapper.Map<BorrowDetail>(borrowDetailDto);
            borrowDetail.Id = id;

            var result = await _borrowDetailService.UpdateBorrowDetail(borrowDetail);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int?[] id = null)
        {
            var result = await _borrowDetailService.DeleteBorrowDetail(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
