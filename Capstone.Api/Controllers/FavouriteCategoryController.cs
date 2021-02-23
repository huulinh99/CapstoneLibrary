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
using Capstone.Core.Interfaces.FavouriteCategoryInterfaces;
using Capstone.Core.QueryFilters;
using Capstone.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Capstone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouriteCategoryController : ControllerBase
    {
        private readonly IFavouriteCategoryService _favouriteCategoryService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        public FavouriteCategoryController(IFavouriteCategoryService favouriteCategoryService, IMapper mapper, IUriService uriService)
        {
            _favouriteCategoryService = favouriteCategoryService;
            _mapper = mapper;
            _uriService = uriService;
        }
        [HttpGet(Name = nameof(GetFavouriteCategories))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<FavouriteCategoryDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetFavouriteCategories([FromQuery] FavouriteCategoryQueryFilter filters)
        {
            var favouriteCategories = _favouriteCategoryService.GetFavouriteCategories(filters);
            var favouriteCategoriesDtos = _mapper.Map<IEnumerable<FavouriteCategoryDto>>(favouriteCategories);
            var metadata = new Metadata
            {
                TotalCount = favouriteCategories.TotalCount,
                PageSize = favouriteCategories.PageSize,
                CurrentPage = favouriteCategories.CurrentPage,
                TotalPages = favouriteCategories.TotalPages,
                HasNextPage = favouriteCategories.HasNextPage,
                HasPreviousPage = favouriteCategories.HasPreviousPage,
            };

            var response = new ApiResponse<IEnumerable<FavouriteCategoryDto>>(favouriteCategoriesDtos)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetFavouriteCategory(int id)
        {
            var favouriteCategory = _favouriteCategoryService.GetFavouriteCategory(id);
            var favouriteCategoryDto = _mapper.Map<FavouriteCategoryDto>(favouriteCategory);
            var response = new ApiResponse<FavouriteCategoryDto>(favouriteCategoryDto);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult FavouriteCategory(FavouriteCategoryDto favouriteCategoryDto)
        {
             _favouriteCategoryService.InsertFavouriteCategory(favouriteCategoryDto);
            var response = new ApiResponse<FavouriteCategoryDto>(favouriteCategoryDto);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Put(int id, FavouriteCategoryDto favouriteCategoryDto)
        {
            var favouriteCategory = _mapper.Map<FavouriteCategory>(favouriteCategoryDto);
            favouriteCategory.Id = id;

            var result = _favouriteCategoryService.UpdateFavouriteCategory(favouriteCategory);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] int?[] id = null)
        {
            var result = _favouriteCategoryService.DeleteFavouriteCategory(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
