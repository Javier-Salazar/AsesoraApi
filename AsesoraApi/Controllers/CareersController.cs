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
    [Route("/api/careers")]
    public class CareersController : Controller
    {
        private readonly AppDbContext context;

        public CareersController(AppDbContext context)
        {
            this.context = context;
        }

        // GET api/<CareersController>
        [HttpGet]
        public async Task<ActionResult<List<Career>>> Get()
        {
            try
            {
                var careers = await (from Career in context.Career
                                     join School in context.School
                                       on Career.career_school equals School.school_code
                                     where School.school_status == 'A'
                                     select new
                                     {
                                         Career.career_code,
                                         Career.career_name,
                                         Career.career_school,
                                         School.school_name,
                                         Career.career_status
                                     }).ToListAsync();

                return Ok(careers);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/<CareersController>/{id}
        [HttpGet("{id}", Name = "GetCareer")]
        public async Task<ActionResult<Career>> Get(String id)
        {
            try
            {
                var career = await (from Career in context.Career
                                    join School in context.School
                                      on Career.career_school equals School.school_code
                                    where School.school_status == 'A'
                                    select new
                                    {
                                        Career.career_code,
                                        Career.career_name,
                                        Career.career_school,
                                        School.school_name,
                                        Career.career_status
                                    }).FirstOrDefaultAsync(code => code.career_code == id);

                if (career == null)
                {
                    return NotFound();
                }

                return Ok(career);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/<CareersController>
        [HttpPost]
        public async Task<ActionResult> Post(Career career)
        {
            try
            {
                context.Career.Add(career);

                await context.SaveChangesAsync();

                return new CreatedAtRouteResult("GetCareer", new { id = career.career_code }, career);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<CareersController>{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(String id, Career career)
        {
            try
            {
                if (career == null)
                {
                    return NotFound();
                }
                else if (career.career_code == id)
                {
                    context.Entry(career).State = EntityState.Modified;

                    await context.SaveChangesAsync();

                    return Ok(career);
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

        // DELETE api/<CareersController>/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(String id)
        {
            try
            {
                var career = await context.Career.FirstOrDefaultAsync(code => code.career_code == id);

                if (career == null)
                {
                    return NotFound();
                }
                else
                {
                    context.Career.Remove(career);

                    await context.SaveChangesAsync();

                    return Ok(career);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
