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
    [Route("/api/advisors")]
    public class AdvisorsController : Controller
    {
        private readonly AppDbContext context;

        public AdvisorsController(AppDbContext context)
        {
            this.context = context;
        }

        // GET api/<AdvisorsController>
        [HttpGet]
        public async Task<ActionResult<List<Advisor>>> Get()
        {
            try
            {
                var advisors = await (from Advisor in context.Advisor
                                      join Userx in context.Userx
                                        on Advisor.advisor_code equals Userx.userx_code
                                      where Userx.userx_type == 'A' //&& Userx.userx_status == 'A'
                                      select new
                                      {
                                          Advisor.advisor_code,
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
                                          Advisor.advisor_rating,
                                          Advisor.advisor_comments,
                                          Advisor.advisor_status
                                      }).ToListAsync();

                return Ok(advisors);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/<AdvisorsController>/{id}
        [HttpGet("{id}", Name = "GetAdvisor")]
        public async Task<ActionResult<Advisor>> Get(String id)
        {
            try
            {
                var advisor = await (from Advisor in context.Advisor
                                     join Userx in context.Userx
                                       on Advisor.advisor_code equals Userx.userx_code
                                     where Userx.userx_type == 'A' && Userx.userx_status == 'A'
                                     select new
                                   {
                                         Advisor.advisor_code,
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
                                          Advisor.advisor_rating,
                                          Advisor.advisor_comments,
                                          Advisor.advisor_status
                                   }).FirstOrDefaultAsync(code => code.advisor_code == id);

                if (advisor == null)
                {
                    return NotFound();
                }

                return Ok(advisor);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/<AdvisorsController>
        [HttpPost]
        public async Task<ActionResult> Post(Advisor advisor)
        {
            try
            {
                context.Advisor.Add(advisor);

                await context.SaveChangesAsync();

                return new CreatedAtRouteResult("GetAdvisor", new { id = advisor.advisor_code }, advisor);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<AdvisorsController>{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(String id, Advisor advisor)
        {
            try
            {
                if (advisor == null)
                {
                    return NotFound();
                }
                else if (advisor.advisor_code == id)
                {
                    context.Entry(advisor).State = EntityState.Modified;

                    await context.SaveChangesAsync();

                    return Ok(advisor);
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

        // DELETE api/<AdvisorsController>/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(String id)
        {
            try
            {
                var advisor = await context.Advisor.FirstOrDefaultAsync(code => code.advisor_code == id);

                if (advisor == null)
                {
                    return NotFound();
                }
                else
                {
                    context.Advisor.Remove(advisor);

                    await context.SaveChangesAsync();

                    return Ok(advisor);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
