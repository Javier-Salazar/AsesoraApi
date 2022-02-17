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
    [Route("/api/majors")]
    public class MajorsController : Controller
    {
        private readonly AppDbContext context;

        public MajorsController(AppDbContext context)
        {
            this.context = context;
        }

        // GET api/<MajorsController>
        [HttpGet]
        public async Task<ActionResult<List<Major>>> Get()
        {
            try
            {
                var majors = await (from Major in context.Major
                                    join Career in context.Career
                                      on Major.major_career equals Career.career_code
                                    join School in context.School
                                      on Career.career_school equals School.school_code
                                    where Career.career_status == 'A' && School.school_status == 'A'
                                    select new
                                     {
                                        Major.major_code,
                                        Major.major_name,
                                        Major.major_career,
                                        Career.career_name,
                                        School.school_name,
                                        Major.major_status
                                     }).ToListAsync();

                return Ok(majors);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/<MajorsController>/{id}
        [HttpGet("{id}", Name = "GetMajor")]
        public async Task<ActionResult<Major>> Get(String id)
        {
            try
            {
                var major = await (from Major in context.Major
                                   join Career in context.Career
                                     on Major.major_career equals Career.career_code
                                   join School in context.School
                                     on Career.career_school equals School.school_code
                                   where Career.career_status == 'A' && School.school_status == 'A'
                                   select new
                                   {
                                       Major.major_code,
                                       Major.major_name,
                                       Major.major_career,
                                       Career.career_name,
                                       School.school_name,
                                       Major.major_status
                                   }).FirstOrDefaultAsync(code => code.major_code == id);

                if (major == null)
                {
                    return NotFound();
                }

                return Ok(major);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/<MajorsController>
        [HttpPost]
        public async Task<ActionResult> Post(Major major)
        {
            try
            {
                context.Major.Add(major);

                await context.SaveChangesAsync();

                return new CreatedAtRouteResult("GetMajor", new { id = major.major_code }, major);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<MajorsController>{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(String id, Major major)
        {
            try
            {
                if (major == null)
                {
                    return NotFound();
                }
                else if (major.major_code == id)
                {
                    context.Entry(major).State = EntityState.Modified;

                    await context.SaveChangesAsync();

                    return Ok(major);
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

        // DELETE api/<MajorsController>/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(String id)
        {
            try
            {
                var major = await context.Major.FirstOrDefaultAsync(code => code.major_code == id);

                if (major == null)
                {
                    return NotFound();
                }
                else
                {
                    context.Major.Remove(major);

                    await context.SaveChangesAsync();

                    return Ok(major);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
