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
    [Route("/api/props")]
    public class PropsController : Controller
    {
        private readonly AppDbContext context;

        public PropsController(AppDbContext context)
        {
            this.context = context;
        }

        // GET api/<PropsController>
        [HttpGet]
        public async Task<ActionResult<List<Props>>> Get()
        {
            try
            {
                var props = await context.Props.ToListAsync();

                return Ok(props);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/<PropsController>/{id}
        [HttpGet("{id}", Name = "GetProp")]
        public async Task<ActionResult<Props>> Get(String id)
        {
            try
            {
                var prop = await context.Props.FirstOrDefaultAsync(code => code.props_program == id);

                if (prop == null)
                {
                    return NotFound();
                }

                return Ok(prop);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/<PropsController>
        [HttpPost]
        public async Task<ActionResult> Post(Props prop)
        {
            try
            {
                context.Props.Add(prop);

                await context.SaveChangesAsync();

                return new CreatedAtRouteResult("GetProp", new { id = prop.props_program }, prop);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<PropsController>{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(String id, Props prop)
        {
            try
            {
                if (prop == null)
                {
                    return NotFound();
                }
                else if (prop.props_program == id)
                {
                    context.Entry(prop).State = EntityState.Modified;

                    await context.SaveChangesAsync();

                    return Ok(prop);
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

        // DELETE api/<PropsController>/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(String id)
        {
            try
            {
                var prop = await context.Props.FirstOrDefaultAsync(code => code.props_program == id);

                if (prop == null)
                {
                    return NotFound();
                }
                else
                {
                    context.Props.Remove(prop);

                    await context.SaveChangesAsync();

                    return Ok(prop);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
