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
    [Route("/api/subjects")]
    public class SubjectsController : Controller
    {
        private readonly AppDbContext context;

        public SubjectsController(AppDbContext context)
        {
            this.context = context;
        }

        // GET api/<SubjectsController>
        [HttpGet]
        public async Task<ActionResult<List<Subjectx>>> Get()
        {
            try
            {
                var subjects = await (from Subjectx in context.Subjectx
                                      join Career in context.Career
                                        on Subjectx.subjectx_career equals Career.career_code
                                      join Major in context.Major
                                        on Subjectx.subjectx_major equals Major.major_code
                                      join Classroom in context.Classroom
                                        on Subjectx.subjectx_classroom equals Classroom.classroom_code
                                      where Career.career_status == 'A' && Major.major_status == 'A'
                                      select new
                                      {
                                          Subjectx.subjectx_id,
                                          Subjectx.subjectx_code,
                                          Subjectx.subjectx_name,
                                          Subjectx.subjectx_credits,
                                          Subjectx.subjectx_career,
                                          Career.career_name,
                                          Subjectx.subjectx_major,
                                          Major.major_name,
                                          Subjectx.subjectx_classroom,
                                          Classroom.classroom_name,
                                          Subjectx.subjectx_status,
                                      }).ToListAsync();

                return Ok(subjects);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/<SubjectsController>/{id}
        [HttpGet("{id}", Name = "GetSubject")]
        public async Task<ActionResult<Subjectx>> Get(int id)
        {
            try
            {
                var subject = await (from Subjectx in context.Subjectx
                                     join Career in context.Career
                                       on Subjectx.subjectx_career equals Career.career_code
                                     join Major in context.Major
                                       on Subjectx.subjectx_major equals Major.major_code
                                     join Classroom in context.Classroom
                                       on Subjectx.subjectx_classroom equals Classroom.classroom_code
                                     where Career.career_status == 'A' && Major.major_status == 'A'
                                     select new
                                     {
                                         Subjectx.subjectx_id,
                                         Subjectx.subjectx_code,
                                         Subjectx.subjectx_name,
                                         Subjectx.subjectx_credits,
                                         Subjectx.subjectx_career,
                                         Career.career_name,
                                         Subjectx.subjectx_major,
                                         Major.major_name,
                                         Subjectx.subjectx_classroom,
                                         Classroom.classroom_name,
                                         Subjectx.subjectx_status,
                                     }).FirstOrDefaultAsync(code => code.subjectx_id == id);

                if (subject == null)
                {
                    return NotFound();
                }

                return Ok(subject);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/<SubjectsController>
        [HttpPost]
        public async Task<ActionResult> Post(Subjectx subject)
        {
            try
            {
                context.Subjectx.Add(subject);

                await context.SaveChangesAsync();

                return new CreatedAtRouteResult("GetSubject", new { id = subject.subjectx_id }, subject);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<SubjectsController>{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Subjectx subject)
        {
            try
            {
                if (subject == null)
                {
                    return NotFound();
                }
                else if (subject.subjectx_id == id)
                {
                    context.Entry(subject).State = EntityState.Modified;

                    await context.SaveChangesAsync();

                    return Ok(subject);
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

        // DELETE api/<SubjectsController>/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var subject = await context.Subjectx.FirstOrDefaultAsync(code => code.subjectx_id == id);

                if (subject == null)
                {
                    return NotFound();
                }
                else
                {
                    context.Subjectx.Remove(subject);

                    await context.SaveChangesAsync();

                    return Ok(subject);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
