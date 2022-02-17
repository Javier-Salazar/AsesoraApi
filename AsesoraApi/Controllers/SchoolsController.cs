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
    [Route("/api/schools")]
    public class SchoolsController : Controller
    {
        private readonly AppDbContext context;

        public SchoolsController(AppDbContext context)
        {
            this.context = context;
        }

        // GET api/<SchoolsController>
        [HttpGet]
        public async Task<ActionResult<List<School>>> Get()
        {
            try
            {
                var schools = await context.School.ToListAsync();

                return Ok(schools);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/<SchoolsController>/{id}
        [HttpGet("{id}", Name = "GetSchool")]
        public async Task<ActionResult<School>> Get(String id)
        {
            try
            {
                var school = await context.School.FirstOrDefaultAsync(code => code.school_code == id);

                if (school == null)
                {
                    return NotFound();
                }

                return Ok(school);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/<SchoolsController>
        [HttpPost]
        public async Task<ActionResult> Post(School school)
        {
            try
            {
                context.School.Add(school);

                await context.SaveChangesAsync();

                return new CreatedAtRouteResult("GetSchool", new { id = school.school_code }, school);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<SchoolsController>{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(String id, School school)
        {
            try
            {
                if (school == null)
                {
                    return NotFound();
                }
                else if (school.school_code == id)
                {
                    context.Entry(school).State = EntityState.Modified;

                    await context.SaveChangesAsync();

                    return Ok(school);
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

        // DELETE api/<SchoolsController>/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(String id)
        {
            try
            {
                var school = await context.School.FirstOrDefaultAsync(code => code.school_code == id);

                if (school == null)
                {
                    return NotFound();
                }
                else
                {
                    context.School.Remove(school);

                    await context.SaveChangesAsync();

                    return Ok(school);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
