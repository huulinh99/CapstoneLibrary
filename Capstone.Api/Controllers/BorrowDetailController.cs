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
using Capstone.Core.QueryFilters;
using Capstone.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Capstone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowDetailController : ControllerBase
    {
        private readonly IBorrowDetailService _borrowBookService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        public BorrowDetailController(IBorrowDetailService borrowDetailService, IMapper mapper, IUriService uriService)
        {
            _borrowBookService = borrowDetailService;
            _mapper = mapper;
            _uriService = uriService;
            //gg
        }
        [HttpGet(Name = nameof(GetBorrowDetails))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<BorrowDetailDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetBorrowDetails([FromQuery] BorrowDetailQueryFilter filters)
        {
            var borrowDetails = _borrowBookService.GetBorrowDetails(filters);
            var borrowDetailsDtos = _mapper.Map<IEnumerable<BorrowDetailDto>>(borrowDetails);
            var metadata = new Metadata
            {
                TotalCount = borrowDetails.TotalCount,
                PageSize = borrowDetails.PageSize,
                CurrentPage = borrowDetails.CurrentPage,
                TotalPages = borrowDetails.TotalPages,
                HasNextPage = borrowDetails.HasNextPage,
                HasPreviousPage = borrowDetails.HasPreviousPage,
                NextPageUrl = _uriService.GetBorrowDetailPaginationUri(filters, Url.RouteUrl(nameof(GetBorrowDetails))).ToString(),
                PreviousPageUrl = _uriService.GetBorrowDetailPaginationUri(filters, Url.RouteUrl(nameof(GetBorrowDetails))).ToString()
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
            var borrowDetail = await _borrowBookService.GetBorrowDetail(id);
            var borrowDetailDto = _mapper.Map<BorrowDetailDto>(borrowDetail);
            var response = new ApiResponse<BorrowDetailDto>(borrowDetailDto);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> BorrowDetail(BorrowDetailDto borrowDetailDto)
        {
            var borrowDetail = _mapper.Map<BorrowDetail>(borrowDetailDto);
            await _borrowBookService.InsertBorrowDetail(borrowDetail);
            borrowDetailDto = _mapper.Map<BorrowDetailDto>(borrowDetail);
            var response = new ApiResponse<BorrowDetailDto>(borrowDetailDto);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, BorrowDetailDto borrowDetailDto)
        {
            var borrowDetail = _mapper.Map<BorrowDetail>(borrowDetailDto);
            borrowDetail.Id = id;

            var result = await _borrowBookService.UpdateBorrowDetail(borrowDetail);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpPut("Delete")]
        public async Task<IActionResult> Delete(int[] id)
        {
            var result = await _borrowBookService.DeleteBorrowDetail(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
