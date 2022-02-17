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
    [Route("/api/buildings")]
    public class BuildingsController : Controller
    {
        private readonly AppDbContext context;

        public BuildingsController(AppDbContext context)
        {
            this.context = context;
        }

        // GET api/<BuildingsController>
        [HttpGet]
        public async Task<ActionResult<List<Building>>> Get()
        {
            try
            {
                var buildings = await (from Building in context.Building
                                       join School in context.School 
                                         on Building.building_school equals School.school_code
                                       where School.school_status == 'A'
                                       select new
                                       {
                                           Building.building_code,
                                           Building.building_name,
                                           Building.building_school,
                                           School.school_name,
                                           Building.building_status
                                       }).ToListAsync();

                return Ok(buildings);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/<BuildingsController>/{id}
        [HttpGet("{id}", Name = "GetBuilding")]
        public async Task<ActionResult<Building>> Get(String id)
        {
            try
            {
                var building = await (from Building in context.Building
                                      join School in context.School
                                        on Building.building_school equals School.school_code
                                      where School.school_status == 'A'
                                      select new
                                      {
                                          Building.building_code,
                                          Building.building_name,
                                          Building.building_school,
                                          School.school_name,
                                          Building.building_status
                                      }).FirstOrDefaultAsync(code => code.building_code == id);

                if (building == null)
                {
                    return NotFound();
                }

                return Ok(building);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/<BuildingsController>
        [HttpPost]
        public async Task<ActionResult> Post(Building building)
        {
            try
            {
                context.Building.Add(building);

                await context.SaveChangesAsync();

                return new CreatedAtRouteResult("GetBuilding", new { id = building.building_code }, building);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<BuildingsController>{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(String id, Building building)
        {
            try
            {
                if (building == null)
                {
                    return NotFound();
                }
                else if (building.building_code == id)
                {
                    context.Entry(building).State = EntityState.Modified;

                    await context.SaveChangesAsync();

                    return Ok(building);
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

        // DELETE api/<BuildingsController>/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(String id)
        {
            try
            {
                var building = await context.Building.FirstOrDefaultAsync(code => code.building_code == id);

                if (building == null)
                {
                    return NotFound();
                }
                else
                {
                    context.Building.Remove(building);

                    await context.SaveChangesAsync();

                    return Ok(building);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
