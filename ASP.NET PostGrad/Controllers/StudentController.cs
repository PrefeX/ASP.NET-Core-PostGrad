using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ASP.NET_PostGrad.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_PostGrad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly PostGradContext context;

        public StudentController(PostGradContext context)
        {
            this.context = context;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetAllStudent()
        {
            return this.context.Students;
        }

        [HttpGet("{id}")]
        public ActionResult<Student> GetStudentById(int id)
        {
            var student = context.Students.Find(id);

            if (student == null)
                return NotFound("The student was not found");
            else
                return student;
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteStudentById(int id)
        {
            var student = context.Students.Find(id);

            if (student == null)
                return NotFound("The student was not found");
            else
            {
                context.Remove(student);
                context.SaveChanges();

                return Ok("The user have been deleted");
            }
        }

        [HttpPut]
        public ActionResult<Student> UpdateStudent(Student student)
        {
            // Does the id have a default value? If so, it's not been set
            if (student.Id == 0)
                return BadRequest("ID not provided");

            // Do we actually have any students with the provided ID?
            int studentsWithId = context.Students.Where(s => s.Id == student.Id).Count();
            if (studentsWithId == 0)
                return NotFound("No students with that ID!");

            // Do we actually have any superisors with the provided ID?
            int supervisorsWithId = context.Supervisors.Where(s => s.Id == student.SupervisorId).Count();
            if (supervisorsWithId == 0)
                return NotFound("No supervisors with that ID!");

            // Check for duplicate supervisor assignements (it is a 1-1 DB relationship)
            int studentsWithSuper = context.Students.Where(s => s.SupervisorId == student.SupervisorId).Count();
            if (studentsWithSuper > 0)
                return Conflict("Super is already assigned!");


            context.Update(student);
            context.SaveChanges();

            return student;
        }

        [HttpPost]
        public ActionResult<Student> AddStudent(Student student)
        {
            // Does the id have a default value? If not, the user already exists!
            if (student.Id != 0)
                return BadRequest("ID is provided");

            // Check if there is an assigned sueprvisor id before running related error checks
            if (student.SupervisorId != null)
            {
                // Do we actually have any superisors with the provided ID?
                int supervisorsWithId = context.Supervisors.Where(s => s.Id == student.SupervisorId).Count();
                if (supervisorsWithId == 0)
                    return NotFound("No supervisors with that ID!");

                // Check for duplicate supervisor assignements (it is a 1-1 DB relationship)
                int studentsWithSuper = context.Students.Where(s => s.SupervisorId == student.SupervisorId).Count();
                if (studentsWithSuper > 0)
                    return Conflict("Super is already assigned!");
            }

            context.Add(student);
            context.SaveChanges();

            return student;
        }

    }
}