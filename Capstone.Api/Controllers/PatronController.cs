using AutoMapper;
using Capstone.Api.Respones;
using Capstone.Core.CustomEntities;
using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Core.QueryFilters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatronController : Controller
    {
        private readonly IPatronService _patronService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IUriService _uriService;
        public PatronController(IPatronService patronService, IConfiguration configuration, IMapper mapper, IUriService uriService)
        {
            _patronService = patronService;
            _mapper = mapper;
            _uriService = uriService;
            _configuration = configuration;
        }
        [HttpGet(Name = nameof(GetPatrons))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<PatronDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetPatrons([FromQuery] PatronQueryFilter filters)
        {
            var books = _patronService.GetPatrons(filters);
            var booksDtos = _mapper.Map<IEnumerable<PatronDto>>(books);
            var metadata = new Metadata
            {
                TotalCount = books.TotalCount,
                PageSize = books.PageSize,
                CurrentPage = books.CurrentPage,
                TotalPages = books.TotalPages,
                HasNextPage = books.HasNextPage,
                HasPreviousPage = books.HasPreviousPage
            };

            var response = new ApiResponse<IEnumerable<PatronDto>>(booksDtos)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetPatron(int id)
        {
            var patron = _patronService.GetPatron(id);
            var patronDto = _mapper.Map<PatronDto>(patron);
            var response = new ApiResponse<PatronDto>(patronDto);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Patron(PatronDto patronDto)
        {

            var tmp = _patronService.GetPatron(patronDto.Email);
            if (tmp == null)
            {
                var patron = _mapper.Map<Patron>(patronDto);
                _patronService.InsertPatron(patron);
                patronDto = _mapper.Map<PatronDto>(patron);
                var response = new ApiResponse<PatronDto>(patronDto);
                return Ok(response);
            }
            else
            {
                var token = GenerateToken(patronDto);
                return Ok(new { token });
            }
        }

        [HttpPut]
        public IActionResult Put(int id, PatronDto patronDto)
        {
            var patron = _mapper.Map<Patron>(patronDto);
            patron.Id = id;
            var result = _patronService.UpdatePatron(patron);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        private string GenerateToken(PatronDto patronDto)
        {
            //Headers
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            //Claims
            var claims = new[]
            {
                new Claim("id", patronDto.Id.ToString()),
                new Claim("name", patronDto.Name),
                new Claim("address", patronDto.Address),
                new Claim("DoB", patronDto.DoB.ToString()),
                new Claim("email", patronDto.Email),
                new Claim("gender", patronDto.Gender),
                new Claim("phone", patronDto.Phone)
            };

            //Payloads
            var payload = new JwtPayload
            (
               _configuration["Authentication:Issuer"],
               _configuration["Authentication:Audience"],
               claims,
               DateTime.UtcNow,
               DateTime.UtcNow.AddMinutes(10)

            );

            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] int?[] id = null)
        {
            var result =  _patronService.DeletePatron(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
