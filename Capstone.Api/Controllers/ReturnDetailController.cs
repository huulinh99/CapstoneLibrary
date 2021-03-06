﻿using System;
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
using Capstone.Core.QueryFilters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Capstone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReturnDetailController : Controller
    {
        private readonly IReturnDetailService _returnDetailService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        public ReturnDetailController(IReturnDetailService returnDetailService, IMapper mapper, IUriService uriService)
        {
            _returnDetailService = returnDetailService;
            _mapper = mapper;
            _uriService = uriService;
        }
        [HttpGet(Name = nameof(GetReturnDetails))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<ReturnDetailDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetReturnDetails([FromQuery] ReturnDetailQueryFilter filters)
        {
            var returnDetails = _returnDetailService.GetReturnDetails(filters);
            var returnDetailsDtos = _mapper.Map<IEnumerable<ReturnDetailDto>>(returnDetails);
            var metadata = new Metadata
            {
                TotalCount = returnDetails.TotalCount,
                PageSize = returnDetails.PageSize,
                CurrentPage = returnDetails.CurrentPage,
                TotalPages = returnDetails.TotalPages,
                HasNextPage = returnDetails.HasNextPage,
                HasPreviousPage = returnDetails.HasPreviousPage
            };

            var response = new ApiResponse<IEnumerable<ReturnDetailDto>>(returnDetailsDtos)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetReturnDetail(int id)
        {
            var returnDetail = _returnDetailService.GetReturnDetail(id);
            var returnDetailDto = _mapper.Map<ReturnDetailDto>(returnDetail);
            var response = new ApiResponse<ReturnDetailDto>(returnDetailDto);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult ReturnDetail(ReturnDetailDto returnDetailDto)
        {
            var returnDetail = _mapper.Map<ReturnDetail>(returnDetailDto);
            _returnDetailService.InsertReturnDetail(returnDetail);
            returnDetailDto = _mapper.Map<ReturnDetailDto>(returnDetail);
            var response = new ApiResponse<ReturnDetailDto>(returnDetailDto);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Put(int id, ReturnDetailDto returnDetailDto)
        {
            var returnDetail = _mapper.Map<ReturnDetail>(returnDetailDto);
            returnDetail.Id = id;

            var result = _returnDetailService.UpdateReturnDetail(returnDetail);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] int?[] id = null)
        {
            var result = _returnDetailService.DeleteReturnDetail(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
