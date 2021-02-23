﻿using AutoMapper;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Capstone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private readonly ICampaignService _campaignService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        public CampaignController(ICampaignService campaignService, IMapper mapper, IUriService uriService)
        {
            _campaignService = campaignService;
            _mapper = mapper;
            _uriService = uriService;
        }
        [HttpGet(Name = nameof(GetCampaigns))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<CampaignDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetCampaigns([FromQuery] CampaignQueryFilter filters)
        {
            var campaigns = _campaignService.GetCampaigns(filters);
            var campaignsDtos = _mapper.Map<IEnumerable<CampaignDto>>(campaigns);
            var metadata = new Metadata
            {
                TotalCount = campaigns.TotalCount,
                PageSize = campaigns.PageSize,
                CurrentPage = campaigns.CurrentPage,
                TotalPages = campaigns.TotalPages,
                HasNextPage = campaigns.HasNextPage,
                HasPreviousPage = campaigns.HasPreviousPage
            };

            var response = new ApiResponse<IEnumerable<CampaignDto>>(campaignsDtos)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetCampaign(int id)
        {
            var campaign = _campaignService.GetCampaign(id);
            var campaignDto = _mapper.Map<CampaignDto>(campaign);
            var response = new ApiResponse<CampaignDto>(campaignDto);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Campaign(CampaignDto campaignDto)
        {
            var post = _mapper.Map<Campaign>(campaignDto);
            _campaignService.InsertCampaign(post);
            campaignDto = _mapper.Map<CampaignDto>(post);
            var response = new ApiResponse<CampaignDto>(campaignDto);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Put(int id, CampaignDto campaignDto)
        {
            var campaign = _mapper.Map<Campaign>(campaignDto);
            campaign.Id = id;

            var result = _campaignService.UpdateCampaign(campaign);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] int?[] id = null)
        {
            var result = _campaignService.DeleteCampaign(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
