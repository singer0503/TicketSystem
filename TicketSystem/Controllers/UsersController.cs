using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSystem.Entities;
using TicketSystem.Models;
using TicketSystem.Services;

namespace TicketSystem.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                // 只允許 Admin 訪問其他使用者資料
                var currentUserId = int.Parse(User.Identity.Name); // 解出 JWT id
                if (id != currentUserId && !User.IsInRole(Role.Admin))
                    return Forbid(); // return 403

                var user = _userService.GetById(id);

                if (user == null)
                    return NotFound();

                return Ok(user);
            }
            catch (Exception) {
                return Forbid();
            }
        }
    }
}
