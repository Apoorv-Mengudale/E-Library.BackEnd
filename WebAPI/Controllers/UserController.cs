﻿using Application.Authorization;
using Application.Services;
using Domain.Models;
using Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(
            IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(RegisterRequest model)
        {
            _userService.Register(model);
            return Ok(new { message = "Registration successful" });
        }

        [Authorize(Role.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [Authorize(Role.Admin)]
        [HttpGet($"GetById/{{id:int}}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);
            return Ok(user);
        }

        [HttpPut($"{{id:int}}")]
        public IActionResult Update(int id, UpdateRequest model)
        {
            _userService.Update(id, model);
            return Ok(new { message = "User updated successfully" });
        }

        [HttpDelete($"{{id:int}}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok(new { message = "User deleted successfully" });
        }

        [Authorize(Role.Admin)]
        [HttpGet("GetUsersPaginated")]
        public IActionResult GetUsersPaginated(int pageNumber, int pageSize)
        {
            var res = _userService._GetUsersPaginated(pageNumber, pageSize);
            return Ok(new { data = res, totalCount = _userService.GetAll().Count() });
        }
    }
}
