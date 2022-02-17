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
    [Route("/api/classrooms")]
    public class ClassroomsController : Controller
    {
        private readonly AppDbContext context;

        public ClassroomsController(AppDbContext context)
        {
            this.context = context;
        }

        // GET api/<ClassroomsController>
        [HttpGet]
        public async Task<ActionResult<List<Classroom>>> Get()
        {
            try
            {
                var classrooms = await (from Classroom in context.Classroom
                                        join Building in context.Building
                                          on Classroom.classroom_building equals Building.building_code
                                        join School in context.School
                                          on Building.building_school equals School.school_code
                                        where Building.building_status == 'A' && School.school_status == 'A'
                                        select new
                                      {
                                            Classroom.classroom_code,
                                            Classroom.classroom_name,
                                            Classroom.classroom_building,
                                            Building.building_name,
                                            School.school_name
                                      }).ToListAsync();

                return Ok(classrooms);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/<ClassroomsController>/{id}
        [HttpGet("{id}", Name = "GetClassroom")]
        public async Task<ActionResult<Classroom>> Get(String id)
        {
            try
            {
                var classroom = await (from Classroom in context.Classroom
                                       join Building in context.Building
                                         on Classroom.classroom_building equals Building.building_code
                                       join School in context.School
                                         on Building.building_school equals School.school_code
                                       where Building.building_status == 'A' && School.school_status == 'A'
                                       select new
                                       {
                                           Classroom.classroom_code,
                                           Classroom.classroom_name,
                                           Classroom.classroom_building,
                                           Building.building_name,
                                           School.school_name
                                       }).FirstOrDefaultAsync(code => code.classroom_code == id);

                if (classroom == null)
                {
                    return NotFound();
                }

                return Ok(classroom);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/<ClassroomsController>
        [HttpPost]
        public async Task<ActionResult> Post(Classroom classroom)
        {
            try
            {
                context.Classroom.Add(classroom);

                await context.SaveChangesAsync();

                return new CreatedAtRouteResult("GetClassroom", new { id = classroom.classroom_code }, classroom);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<ClassroomsController>{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(String id, Classroom classroom)
        {
            try
            {
                if (classroom == null)
                {
                    return NotFound();
                }
                else if (classroom.classroom_code == id)
                {
                    context.Entry(classroom).State = EntityState.Modified;

                    await context.SaveChangesAsync();

                    return Ok(classroom);
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

        // DELETE api/<ClassroomsController>/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(String id)
        {
            try
            {
                var classroom = await context.Classroom.FirstOrDefaultAsync(code => code.classroom_code == id);

                if (classroom == null)
                {
                    return NotFound();
                }
                else
                {
                    context.Classroom.Remove(classroom);

                    await context.SaveChangesAsync();

                    return Ok(classroom);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
