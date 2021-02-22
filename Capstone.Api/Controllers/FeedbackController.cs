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
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Capstone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private static IHttpContextAccessor _httpContextAccessor;
        public FeedbackController(IFeedbackService feedbackService, IMapper mapper, IUriService uriService, IHttpContextAccessor httpContextAccessor)
        {
            _feedbackService = feedbackService;
            _mapper = mapper;
            _uriService = uriService;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet(Name = nameof(GetFeedbacks))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<FeedbackDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetFeedbacks([FromQuery] FeedbackQueryFilter filters)
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var feedbacks = _feedbackService.GetFeedbacks(filters);
            //string str = request.QueryString.ToString();
            //string stringBeforeChar = str.Substring(0, str.IndexOf("&"));
            var feedbackDtos = _mapper.Map<IEnumerable<FeedbackDto>>(feedbacks);
            //var nextPage = feedbacks.CurrentPage >= 1 && feedbacks.CurrentPage < feedbacks.TotalCount
            //               ? _uriService.GetPageUri(feedbacks.CurrentPage + 1, feedbacks.PageSize, _uriService.GetFeedbackPaginationUri(filters, Url.RouteUrl(nameof(GetFeedbacks))).ToString() + stringBeforeChar)
            //               : null;
            //var previousPage = feedbacks.CurrentPage - 1 >= 1 && feedbacks.CurrentPage < feedbacks.TotalCount
            //               ? _uriService.GetPageUri(feedbacks.CurrentPage - 1, feedbacks.PageSize, _uriService.GetFeedbackPaginationUri(filters, Url.RouteUrl(nameof(GetFeedbacks))).ToString() + stringBeforeChar)
            //               : null;
            var metadata = new Metadata
            {
                TotalCount = feedbacks.TotalCount,
                PageSize = feedbacks.PageSize,
                CurrentPage = feedbacks.CurrentPage,
                TotalPages = feedbacks.TotalPages,
                HasNextPage = feedbacks.HasNextPage,
                HasPreviousPage = feedbacks.HasPreviousPage
            };

            var response = new ApiResponse<IEnumerable<FeedbackDto>>(feedbackDtos)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeedback(int id)
        {
            var feedback = await _feedbackService.GetFeedback(id);
            var feedbackDto = _mapper.Map<FeedbackDto>(feedback);
            var response = new ApiResponse<FeedbackDto>(feedbackDto);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Book(FeedbackDto feedbackDto)
        {
            var feedback = _mapper.Map<Feedback>(feedbackDto);
            await _feedbackService.InsertFeedback(feedback);
            feedbackDto = _mapper.Map<FeedbackDto>(feedback);
            var response = new ApiResponse<FeedbackDto>(feedbackDto);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, FeedbackDto feedbackDto)
        {
            var feedback = _mapper.Map<Feedback>(feedbackDto);
            feedback.Id = id;

            var result = await _feedbackService.UpdateFeedback(feedback);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int?[] id = null)
        {
            var result = await _feedbackService.DeleteFeedback(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
