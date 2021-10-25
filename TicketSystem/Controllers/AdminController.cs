using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSystem.Entities;
using TicketSystem.Services;

namespace TicketSystem.Controllers
{
    [Authorize(Roles = Role.Admin)]
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpPost]
        public JsonResult Post(User user)
        {
            var result = _userService.Create(user);
            if (result == null)
            {
                return new JsonResult("fail, please check data");
            }
            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(User user)
        {

            int result = _userService.Update(user);
            if (result == 0)
            {
                return new JsonResult("fail, please check data");
            }
            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            int result = _userService.Delete(id);
            if (result == 0)
            {
                return new JsonResult("fail, please check data");
            }
            return new JsonResult("Deleted Successfully");
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
            catch (Exception)
            {
                return Forbid();
            }
        }
    }
}
