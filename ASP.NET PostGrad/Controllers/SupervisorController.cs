using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ASP.NET_PostGrad.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_PostGrad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupervisorController : ControllerBase
    {
        private readonly PostGradContext context;

        public SupervisorController(PostGradContext context)
        {
            this.context = context;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Supervisor>> GetAllSupervisors()
        {
            return this.context.Supervisors;
        }

        [HttpGet("{id}")]

        public ActionResult<Supervisor> GetSupervisorById(int id)
        {
            var supervisor = context.Supervisors.Find(id);

            if (supervisor == null)
                return NotFound("The supervisor was not found");
            else
                return supervisor;
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteSupervisorById(int id)
        {
            var supervisor = context.Supervisors.Find(id);

            if (supervisor == null)
                return NotFound("The supervisor was not found");
            else
            {
                context.Remove(supervisor);
                context.SaveChanges();

                return StatusCode((int)HttpStatusCode.OK);
            }
        }

        [HttpPut]
        public ActionResult<Supervisor> UpdateSupervisor(Supervisor supervisor)
        {
            // Does the id have a default value? If so, it's not been set
            if (supervisor.Id == 0)
                return BadRequest("ID not provided");

            // Do we actually have any supervisors with the provided ID?
            int supervisorsWithId = context.Supervisors.Where(s => s.Id == supervisor.Id).Count();
            if (supervisorsWithId == 0)
                return NotFound("No students with that ID!");


            context.Update(supervisor);
            context.SaveChanges();

            return supervisor;
        }

        [HttpPost]
        public ActionResult<Supervisor> AddSupervisor(Supervisor supervisor)
        {
            // Does the id have a default value? If not, the user already exists!
            if (supervisor.Id != 0)
                return BadRequest("ID is provided");

            context.Add(supervisor);
            context.SaveChanges();

            return supervisor;
        }

    }
}