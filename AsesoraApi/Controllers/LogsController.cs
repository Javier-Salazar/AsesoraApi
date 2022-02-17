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
    [Route("/api/logs")]
    public class LogsController : Controller
    {
        private readonly AppDbContext context;

        public LogsController(AppDbContext context)
        {
            this.context = context;
        }

        // GET api/<LogsController>
        [HttpGet]
        public async Task<ActionResult<List<Log>>> Get()
        {
            try
            {
                var logs = await (from Log in context.Log
                                  join Userx in context.Userx
                                    on Log.log_user equals Userx.userx_code
                                  select new
                                  {
                                      Log.log_id,
                                      Log.log_timestamp,
                                      Log.log_user,
                                      Userx.userx_name,
                                      Log.log_prog,
                                      Log.log_action,
                                      Log.log_info,
                                      Log.log_key
                                  }).ToListAsync();

                return Ok(logs);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/<LogsController>/{id}
        [HttpGet("{id}", Name = "GetLog")]
        public async Task<ActionResult<Log>> Get(int id)
        {
            try
            {
                var log = await (from Log in context.Log
                                join Userx in context.Userx
                                  on Log.log_user equals Userx.userx_code
                                select new
                                {
                                    Log.log_id,
                                    Log.log_timestamp,
                                    Log.log_user,
                                    Userx.userx_name,
                                    Log.log_prog,
                                    Log.log_action,
                                    Log.log_info,
                                    Log.log_key
                                }).FirstOrDefaultAsync(code => code.log_id == id);

                if (log == null)
                {
                    return NotFound();
                }

                return Ok(log);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/<LogsController>
        [HttpPost]
        public async Task<ActionResult> Post(Log log)
        {
            try
            {
                context.Log.Add(log);

                await context.SaveChangesAsync();

                return new CreatedAtRouteResult("GetLog", new { id = log.log_id }, log);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<LogsController>{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Log log)
        {
            try
            {
                if (log == null)
                {
                    return NotFound();
                }
                else if (log.log_id == id)
                {
                    context.Entry(log).State = EntityState.Modified;

                    await context.SaveChangesAsync();

                    return Ok(log);
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

        // DELETE api/<LogsController>/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var log = await context.Log.FirstOrDefaultAsync(code => code.log_id == id);

                if (log == null)
                {
                    return NotFound();
                }
                else
                {
                    context.Log.Remove(log);

                    await context.SaveChangesAsync();

                    return Ok(log);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
