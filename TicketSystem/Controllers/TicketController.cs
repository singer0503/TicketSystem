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
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TicketController : ControllerBase
    {
        private ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var ticketDatas = _ticketService.Get();
            return Ok(ticketDatas);
            //return new JsonResult(ticketDatas);
        }

        [HttpPost]
        [Authorize(Roles = Role.Admin+","+ Role.QA)]
        public JsonResult Post(TicketData ticketData)
        {
            var newTicketData = _ticketService.Create(ticketData);
            if (newTicketData == null) {
                return new JsonResult("fail, please check Summary / Description / Type");
            }

            return new JsonResult("Added Successfully");
        }
    }
}