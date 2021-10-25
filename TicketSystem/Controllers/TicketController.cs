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
    [Authorize]
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
        [Authorize(Roles = Role.Admin+","+ Role.QA + "," +Role.PM)]
        public JsonResult Post(TicketData ticketData)
        {
            var newTicketData = _ticketService.Create(ticketData);
            if (newTicketData == null) {
                return new JsonResult("fail, please check Summary / Description / Type");
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        [Authorize(Roles = Role.Admin + "," + Role.QA + "," + Role.PM)]
        public JsonResult Put(TicketData ticketData)
        {
            
            int result = _ticketService.Update(ticketData);
            if (result == 0)
            {
                return new JsonResult("fail, please check Summary / Description ");
            }

            //return NoContent();
            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Role.Admin + "," + Role.QA + "," + Role.PM)]
        public JsonResult Delete(int id)
        {
            int result = _ticketService.Delete(id);
            if (result == 0)
            {
                return new JsonResult("fail ");
            }
            return new JsonResult("Deleted Successfully");
        }

        [HttpPut]
        [Route("Resolve")]
        [Authorize(Roles = Role.Admin + "," + Role.RD)]
        public JsonResult PutResolve(TicketData ticketData)
        {
            int result = _ticketService.Resolve(ticketData.Id);
            if (result == 0)
            {
                return new JsonResult("Resolve fail ");
            }
            return new JsonResult("Resolve Successfully");
        }
    }
}