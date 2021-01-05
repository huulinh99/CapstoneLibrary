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
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace Capstone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReturnBookController : ControllerBase
    {
        private readonly IReturnBookService _returnBookService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        public ReturnBookController(IReturnBookService returnBookService, IMapper mapper, IUriService uriService)
        {
            _returnBookService = returnBookService;
            _mapper = mapper;
            _uriService = uriService;
        }
        [HttpGet(Name = nameof(GetReturnBooks))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<ReturnBookDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetReturnBooks([FromQuery] ReturnBookQueryFilter filters)
        {
            var returnBooks = _returnBookService.GetReturnBooks(filters);
            var returnBooksDtos = _mapper.Map<IEnumerable<ReturnBookDto>>(returnBooks);
            var metadata = new Metadata
            {
                TotalCount = returnBooks.TotalCount,
                PageSize = returnBooks.PageSize,
                CurrentPage = returnBooks.CurrentPage,
                TotalPages = returnBooks.TotalPages,
                HasNextPage = returnBooks.HasNextPage,
                HasPreviousPage = returnBooks.HasPreviousPage,
                NextPageUrl = _uriService.GetReturnBookPaginationUri(filters, Url.RouteUrl(nameof(GetReturnBooks))).ToString(),
                PreviousPageUrl = _uriService.GetReturnBookPaginationUri(filters, Url.RouteUrl(nameof(GetReturnBooks))).ToString()
            };

            var response = new ApiResponse<IEnumerable<ReturnBookDto>>(returnBooksDtos)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReturnBook(int id)
        {
            var returnBook = await _returnBookService.GetReturnBook(id);
            var returnBookDto = _mapper.Map<ReturnBookDto>(returnBook);
            var response = new ApiResponse<ReturnBookDto>(returnBookDto);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> ReturnBook(ReturnBookDto returnBookDto)
        {
            var returnBook = _mapper.Map<ReturnBook>(returnBookDto);
            await _returnBookService.InsertReturnBook(returnBook);
            returnBookDto = _mapper.Map<ReturnBookDto>(returnBook);
            var response = new ApiResponse<ReturnBookDto>(returnBookDto);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, ReturnBookDto returnBookDto)
        {
            var returnBook = _mapper.Map<ReturnBook>(returnBookDto);
            returnBook.Id = id;

            var result = await _returnBookService.UpdateReturnBook(returnBook);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery]int[]id = null)
        {
            var result = await _returnBookService.DeleteReturnBook(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

    }
}
