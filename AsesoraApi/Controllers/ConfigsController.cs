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
    [Route("/api/configs")]
    public class ConfigsController : Controller
    {
        private readonly AppDbContext context;

        public ConfigsController(AppDbContext context)
        {
            this.context = context;
        }

        // GET api/<ConfigsController>
        [HttpGet]
        public async Task<ActionResult<List<Config>>> Get()
        {
            try
            {
                var configs = await context.Config.ToListAsync();

                return Ok(configs);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/<ConfigsController>/{id}
        [HttpGet("{id}", Name = "GetConfig")]
        public async Task<ActionResult<Config>> Get(String id)
        {
            try
            {
                var config = await context.Config.FirstOrDefaultAsync(code => code.cfg_clave == id);

                if (config == null)
                {
                    return NotFound();
                }

                return Ok(config);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/<ConfigsController>
        [HttpPost]
        public async Task<ActionResult> Post(Config config)
        {
            try
            {
                context.Config.Add(config);

                await context.SaveChangesAsync();

                return new CreatedAtRouteResult("GetConfig", new { id = config.cfg_clave }, config);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<ConfigsController>{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(String id, Config config)
        {
            try
            {
                if (config == null)
                {
                    return NotFound();
                }
                else if (config.cfg_clave == id)
                {
                    context.Entry(config).State = EntityState.Modified;

                    await context.SaveChangesAsync();

                    return Ok(config);
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

        // DELETE api/<ConfigsController>/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(String id)
        {
            try
            {
                var config = await context.Config.FirstOrDefaultAsync(code => code.cfg_clave == id);

                if (config == null)
                {
                    return NotFound();
                }
                else
                {
                    context.Config.Remove(config);

                    await context.SaveChangesAsync();

                    return Ok(config);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
