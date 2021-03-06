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
using Capstone.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Capstone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowBookController : ControllerBase
    {
        private readonly IBorrowBookService _borrowBookService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        public BorrowBookController(IBorrowBookService borrowBookService, IMapper mapper, IUriService uriService)
        {
            _borrowBookService = borrowBookService;
            _mapper = mapper;
            _uriService = uriService;
        }
        [HttpGet(Name = nameof(GetBorrowBooks))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<BorrowBookDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetBorrowBooks([FromQuery] BorrowBookQueryFilter filters)
        {
            var borrowBooks = _borrowBookService.GetBorrowBooks(filters);
            var borrowBooksDtos = _mapper.Map<IEnumerable<BorrowBookDto>>(borrowBooks);
            var metadata = new Metadata
            {
                TotalCount = borrowBooks.TotalCount,
                PageSize = borrowBooks.PageSize,
                CurrentPage = borrowBooks.CurrentPage,
                TotalPages = borrowBooks.TotalPages,
                HasNextPage = borrowBooks.HasNextPage,
                HasPreviousPage = borrowBooks.HasPreviousPage
            };

            var response = new ApiResponse<IEnumerable<BorrowBookDto>>(borrowBooksDtos)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetBorrowBook(int id)
        {
            var borrowBook = _borrowBookService.GetBorrowBook(id);
            var borrowBookDto = _mapper.Map<BorrowBookDto>(borrowBook);
            var response = new ApiResponse<BorrowBookDto>(borrowBookDto);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult BorrowBook(BorrowBookDto borrowBookDto)
        {
            var borrowBook = _mapper.Map<BorrowBook>(borrowBookDto);
            _borrowBookService.InsertBorrowBook(borrowBook);
            borrowBookDto = _mapper.Map<BorrowBookDto>(borrowBook);
            var response = new ApiResponse<BorrowBookDto>(borrowBookDto);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Put(int id, BorrowBookDto borrowBookDto)
        {
            var borrowBook = _mapper.Map<BorrowBook>(borrowBookDto);
            borrowBook.Id = id;

            var result = _borrowBookService.UpdateBorrowBook(borrowBook);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] int?[] id = null)
        {
            var result = _borrowBookService.DeleteBorrowBook(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
