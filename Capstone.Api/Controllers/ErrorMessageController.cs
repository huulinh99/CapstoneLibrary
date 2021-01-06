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
    public class ErrorMessageController : ControllerBase
    {
        //mm
        private readonly IErrorMessageService _errorMessageService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        public ErrorMessageController(IErrorMessageService errorMessageService, IMapper mapper, IUriService uriService)
        {
            _errorMessageService = errorMessageService;
            _mapper = mapper;
            _uriService = uriService;
        }
        [HttpGet(Name = nameof(GetErrorMessages))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<ErrorMessageDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetErrorMessages([FromQuery] ErrorMessageQueryFilter filters)
        {
            var errorMessages = _errorMessageService.GetErrorMessages(filters);
            var errorMessagesDtos = _mapper.Map<IEnumerable<ErrorMessageDto>>(errorMessages);
            var metadata = new Metadata
            {
                TotalCount = errorMessages.TotalCount,
                PageSize = errorMessages.PageSize,
                CurrentPage = errorMessages.CurrentPage,
                TotalPages = errorMessages.TotalPages,
                HasNextPage = errorMessages.HasNextPage,
                HasPreviousPage = errorMessages.HasPreviousPage,
                NextPageUrl = _uriService.GetErrorMessagePaginationUri(filters, Url.RouteUrl(nameof(GetErrorMessages))).ToString(),
                PreviousPageUrl = _uriService.GetErrorMessagePaginationUri(filters, Url.RouteUrl(nameof(GetErrorMessages))).ToString()
            };

            var response = new ApiResponse<IEnumerable<ErrorMessageDto>>(errorMessagesDtos)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetErrorMessage(int id)
        {
            var errorMessage = await _errorMessageService.GetErrorMessage(id);
            var errorMessageDto = _mapper.Map<ErrorMessageDto>(errorMessage);
            var response = new ApiResponse<ErrorMessageDto>(errorMessageDto);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> ErrorMessage(ErrorMessageDto errorMessageDto)
        {
            var post = _mapper.Map<ErrorMessage>(errorMessageDto);
            await _errorMessageService.InsertErrorMessage(post);
            errorMessageDto = _mapper.Map<ErrorMessageDto>(post);
            var response = new ApiResponse<ErrorMessageDto>(errorMessageDto);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, ErrorMessageDto errorMessageDto)
        {
            var errorMessage = _mapper.Map<ErrorMessage>(errorMessageDto);
            errorMessage.Id = id;

            var result = await _errorMessageService.UpdateErrorMessage(errorMessage);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery]int?[]id = null)
        {
            var result = await _errorMessageService.DeleteErrorMessage(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
