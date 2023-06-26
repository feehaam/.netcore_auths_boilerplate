﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace asp.net_core_6_jwt_authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        [HttpGet("/everyone")]
        public IActionResult F1()
        {
            return Ok("This is accessible by everyone.");
        }

        [Authorize]
        [HttpGet("/logged")]
        public IActionResult F2()
        {
            return Ok("This is accessible only by the logged users.");
        }

        [Authorize(Roles = "admin")]
        [HttpGet("/admin")]
        public IActionResult F3()
        {
            return Ok("This is accessible only by the adminstrative users.");
        }

        [Authorize(Roles = "admin,moderator")]
        [HttpGet("/adminmod")]
        public IActionResult F5()
        {
            return Ok("This is accessible for both admin and moderators");
        }


    }
}
