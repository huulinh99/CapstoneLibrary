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
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        public CustomerController(ICustomerService customerService, IMapper mapper, IUriService uriService)
        {
            _customerService = customerService;
            _mapper = mapper;
            _uriService = uriService;
        }
        [HttpGet(Name = nameof(GetCustomers))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<CustomerDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetCustomers([FromQuery] CustomerQueryFilter filters)
        {
            var books = _customerService.GetCustomers(filters);
            var booksDtos = _mapper.Map<IEnumerable<BookDto>>(books);
            var metadata = new Metadata
            {
                TotalCount = books.TotalCount,
                PageSize = books.PageSize,
                CurrentPage = books.CurrentPage,
                TotalPages = books.TotalPages,
                HasNextPage = books.HasNextPage,
                HasPreviousPage = books.HasPreviousPage,
                NextPageUrl = _uriService.GetCustomerPaginationUri(filters, Url.RouteUrl(nameof(GetCustomers))).ToString(),
                PreviousPageUrl = _uriService.GetCustomerPaginationUri(filters, Url.RouteUrl(nameof(GetCustomers))).ToString()
            };

            var response = new ApiResponse<IEnumerable<BookDto>>(booksDtos)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await _customerService.GetCustomer(id);
            var customerDto = _mapper.Map<CustomerDto>(customer);
            var response = new ApiResponse<CustomerDto>(customerDto);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Customer(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            await _customerService.InsertCustomer(customer);
            customerDto = _mapper.Map<CustomerDto>(customer);
            var response = new ApiResponse<CustomerDto>(customerDto);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            customer.Id = id;

            var result = await _customerService.UpdateCustomer(customer);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _customerService.DeleteCustomer(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
