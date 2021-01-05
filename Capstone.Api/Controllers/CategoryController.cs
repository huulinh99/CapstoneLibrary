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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        public CategoryController(ICategoryService categoryService, IMapper mapper, IUriService uriService)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _uriService = uriService;
        }
        [HttpGet(Name = nameof(GetCategories))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<CategoryDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetCategories([FromQuery] CategoryQueryFilter filters)
        {
            var categories = _categoryService.GetCategories(filters);
            var categoriesDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            var metadata = new Metadata
            {
                TotalCount = categories.TotalCount,
                PageSize = categories.PageSize,
                CurrentPage = categories.CurrentPage,
                TotalPages = categories.TotalPages,
                HasNextPage = categories.HasNextPage,
                HasPreviousPage = categories.HasPreviousPage,
                NextPageUrl = _uriService.GetCategoryPaginationUri(filters, Url.RouteUrl(nameof(GetCategories))).ToString(),
                PreviousPageUrl = _uriService.GetCategoryPaginationUri(filters, Url.RouteUrl(nameof(GetCategories))).ToString()
            };

            var response = new ApiResponse<IEnumerable<CategoryDto>>(categoriesDtos)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoryService.GetCategory(id);
            var categoryDto = _mapper.Map<CategoryDto>(category);
            var response = new ApiResponse<CategoryDto>(categoryDto);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Category(CategoryDto categoryDto)
        {
            var post = _mapper.Map<Category>(categoryDto);
            await _categoryService.InsertCategory(post);
            categoryDto = _mapper.Map<CategoryDto>(post);
            var response = new ApiResponse<CategoryDto>(categoryDto);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            category.Id = id;

            var result = await _categoryService.UpdateCategory(category);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery]int[]id = null)
        {
            var result = await _categoryService.DeleteCategory(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
