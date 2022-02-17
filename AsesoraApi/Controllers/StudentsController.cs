using AsesoraApi.Context;
using AsesoraApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsesoraApi.Controllers
{
    [ApiController]
    [Route("/api/students")]
    public class StudentsController : Controller
    {
        private readonly AppDbContext context;

        public StudentsController(AppDbContext context)
        {
            this.context = context;
        }

        // GET api/<StudentsController>
        [HttpGet]
        public async Task<ActionResult<List<Student>>> Get()
        {
            try
            {
                var students = await (from Student in context.Student
                                      join Userx in context.Userx
                                        on Student.student_code equals Userx.userx_code
                                      join School in context.School
                                        on Student.student_school equals School.school_code
                                      join Career in context.Career
                                        on Student.student_career equals Career.career_code
                                      join Major in context.Major
                                        on Student.student_major equals Major.major_code
                                      where Userx.userx_type == 'N' && Userx.userx_status == 'A'
                                      select new
                                    {
                                          Student.student_code,
                                          Userx.userx_name,
                                          Userx.userx_lastname,
                                          Userx.userx_mother_lastname,
                                          Userx.userx_email,
                                          Userx.userx_phone,
                                          Userx.userx_istmp_password,
                                          Userx.userx_date,
                                          Userx.userx_islockedout,
                                          Userx.userx_islockedout_date,
                                          Userx.userx_islockedout_enable_date,
                                          Userx.userx_last_login_date,
                                          Userx.userx_lastfailed_login_date,
                                          Userx.userx_image,
                                          Student.student_school,
                                          School.school_name,
                                          Student.student_career,
                                          Career.career_name,
                                          Student.student_major,
                                          Major.major_name,
                                          Student.student_semester,
                                          Student.student_status
                                          
                                      }).ToListAsync();

                return Ok(students);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/<StudentsController>/{id}
        [HttpGet("{id}", Name = "GetStudent")]
        public async Task<ActionResult<Student>> Get(String id)
        {
            try
            {
                var student = await (from Student in context.Student
                                     join Userx in context.Userx
                                       on Student.student_code equals Userx.userx_code
                                     join School in context.School
                                       on Student.student_school equals School.school_code
                                     join Career in context.Career
                                       on Student.student_career equals Career.career_code
                                     join Major in context.Major
                                       on Student.student_major equals Major.major_code
                                     where Userx.userx_type == 'N' && Userx.userx_status == 'A'
                                     select new
                                     {
                                         Student.student_code,
                                         Userx.userx_name,
                                         Userx.userx_lastname,
                                         Userx.userx_mother_lastname,
                                         Userx.userx_email,
                                         Userx.userx_phone,
                                         Userx.userx_istmp_password,
                                         Userx.userx_date,
                                         Userx.userx_islockedout,
                                         Userx.userx_islockedout_date,
                                         Userx.userx_islockedout_enable_date,
                                         Userx.userx_last_login_date,
                                         Userx.userx_lastfailed_login_date,
                                         Userx.userx_image,
                                         Student.student_school,
                                         School.school_name,
                                         Student.student_career,
                                         Career.career_name,
                                         Student.student_major,
                                         Major.major_name,
                                         Student.student_semester,
                                         Student.student_status
                                     }).FirstOrDefaultAsync(code => code.student_code == id);

                if (student == null)
                {
                    return NotFound();
                }

                return Ok(student);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/<StudentsController>
        [HttpPost]
        public async Task<ActionResult> Post(Student student)
        {
            try
            {
                context.Student.Add(student);

                await context.SaveChangesAsync();

                return new CreatedAtRouteResult("GetStudent", new { id = student.student_code }, student);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<StudentsController>{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(String id, Student student)
        {
            try
            {
                if (student == null)
                {
                    return NotFound();
                }
                else if (student.student_code == id)
                {
                    context.Entry(student).State = EntityState.Modified;

                    await context.SaveChangesAsync();

                    return Ok(student);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/<StudentsController>/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(String id)
        {
            try
            {
                var student = await context.Student.FirstOrDefaultAsync(code => code.student_code == id);

                if (student == null)
                {
                    return NotFound();
                }
                else
                {
                    context.Student.Remove(student);

                    await context.SaveChangesAsync();

                    return Ok(student);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
