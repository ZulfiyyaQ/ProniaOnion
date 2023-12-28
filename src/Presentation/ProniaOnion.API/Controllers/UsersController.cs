﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaOnion.Application.Abstraction.Services;
using ProniaOnion.Application.DTOs.Users;

namespace ProniaOnion.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAuthenticationService _service;

        public UsersController(IAuthenticationService service)
        {
           _service = service;
        }

        [HttpPost]
        public async Task<IActionResult>Register([FromForm]RegisterDto dto)
        {
            await _service.Register(dto);
            return NoContent();
        }
        [HttpPost("[Action]")]
        public async Task<IActionResult> Login([FromForm] LoginDto dto)
        {
            
            return Ok(await _service.Login(dto));
        }

    }
}
