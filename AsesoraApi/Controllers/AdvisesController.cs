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
    [Route("/api/advises")]
    public class AdvisesController : Controller
    {
        private readonly AppDbContext context;

        public AdvisesController(AppDbContext context)
        {
            this.context = context;
        }

        // GET api/<AdvisesController>
        [HttpGet]
        public async Task<ActionResult<List<Advise>>> Get()
        {
            try
            {
                var advises = await (from Advise in context.Advise
                                     join Student in context.Student
                                       on Advise.advise_student equals Student.student_code
                                     join UserStudent in context.Userx
                                       on Student.student_code equals UserStudent.userx_code
                                     join Subjectx in context.Subjectx
                                       on Advise.advise_subject equals Subjectx.subjectx_id
                                     join Advisor in context.Advisor
                                       on Advise.advise_advisor equals Advisor.advisor_code
                                     join UserAdvisor in context.Userx
                                       on Advisor.advisor_code equals UserAdvisor.userx_code
                                     join School in context.School
                                       on Advise.advise_school equals School.school_code
                                     join Building in context.Building
                                       on Advise.advise_building equals Building.building_code
                                     join Classroom in context.Classroom
                                       on Advise.advise_classroom equals Classroom.classroom_code
                                     where UserStudent.userx_status == 'A' && UserStudent.userx_type == 'N'
                                       && Student.student_status == 'A' && Subjectx.subjectx_status == 'A'
                                       && UserAdvisor.userx_status == 'A' && UserAdvisor.userx_type == 'A'
                                       && Advisor.advisor_status == 'A' && School.school_status == 'A'
                                       && Building.building_status == 'A'
                                     select new
                                     {
                                         Advise.advise_code,
                                         Advise.advise_student,
                                         StudentName = UserStudent.userx_name,
                                         StudentLastName = UserStudent.userx_lastname,
                                         StudentLastMotherName = UserStudent.userx_mother_lastname,
                                         StudentEmail = UserStudent.userx_email,
                                         StudentPhone = UserStudent.userx_phone,
                                         StudentImage = UserStudent.userx_image,
                                         Student.student_semester,
                                         Advise.advise_topic,
                                         Advise.advise_subject,
                                         Subjectx.subjectx_name,
                                         Subjectx.subjectx_career,
                                         Subjectx.subjectx_major,
                                         Advise.advise_advisor,
                                         AdvisorName = UserAdvisor.userx_name,
                                         AdvisorLastName = UserAdvisor.userx_lastname,
                                         AdvisorLastMotherName = UserAdvisor.userx_mother_lastname,
                                         AdvisorEmail = UserAdvisor.userx_email,
                                         AdvisorPhone = UserAdvisor.userx_phone,
                                         AdvisorImage = UserAdvisor.userx_image,
                                         Advise.advise_school,
                                         School.school_name,
                                         Advise.advise_building,
                                         Building.building_name,
                                         Advise.advise_classroom,
                                         Classroom.classroom_name,
                                         Advise.advise_date_request,
                                         Advise.advise_date_start,
                                         Advise.advise_date_ends,
                                         Advise.advise_modality,
                                         Advise.advise_url,
                                         Advise.advise_comments,
                                         Advise.advise_status
                                     }).ToListAsync();

                return Ok(advises);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/<AdvisesController>/{id}
        [HttpGet("{id}", Name = "GetAdvise")]
        public async Task<ActionResult<Advise>> Get(int id)
        {
            try
            {
                var advise = await (from Advise in context.Advise
                                    join Student in context.Student
                                      on Advise.advise_student equals Student.student_code
                                    join UserStudent in context.Userx
                                      on Student.student_code equals UserStudent.userx_code
                                    join Subjectx in context.Subjectx
                                      on Advise.advise_subject equals Subjectx.subjectx_id
                                    join Advisor in context.Advisor
                                      on Advise.advise_advisor equals Advisor.advisor_code
                                    join UserAdvisor in context.Userx
                                      on Advisor.advisor_code equals UserAdvisor.userx_code
                                    join School in context.School
                                      on Advise.advise_school equals School.school_code
                                    join Building in context.Building
                                      on Advise.advise_building equals Building.building_code
                                    join Classroom in context.Classroom
                                      on Advise.advise_classroom equals Classroom.classroom_code
                                    where UserStudent.userx_status == 'A' && UserStudent.userx_type == 'N'
                                      && Student.student_status == 'A' && Subjectx.subjectx_status == 'A'
                                      && UserAdvisor.userx_status == 'A' && UserAdvisor.userx_type == 'A'
                                      && Advisor.advisor_status == 'A' && School.school_status == 'A'
                                      && Building.building_status == 'A'
                                    select new
                                    {
                                        Advise.advise_code,
                                        Advise.advise_student,
                                        StudentName = UserStudent.userx_name,
                                        StudentLastName = UserStudent.userx_lastname,
                                        StudentLastMotherName = UserStudent.userx_mother_lastname,
                                        StudentEmail = UserStudent.userx_email,
                                        StudentPhone = UserStudent.userx_phone,
                                        StudentImage = UserStudent.userx_image,
                                        Student.student_semester,
                                        Advise.advise_topic,
                                        Advise.advise_subject,
                                        Subjectx.subjectx_name,
                                        Subjectx.subjectx_career,
                                        Subjectx.subjectx_major,
                                        Advise.advise_advisor,
                                        AdvisorName = UserAdvisor.userx_name,
                                        AdvisorLastName = UserAdvisor.userx_lastname,
                                        AdvisorLastMotherName = UserAdvisor.userx_mother_lastname,
                                        AdvisorEmail = UserAdvisor.userx_email,
                                        AdvisorPhone = UserAdvisor.userx_phone,
                                        AdvisorImage = UserAdvisor.userx_image,
                                        Advise.advise_school,
                                        School.school_name,
                                        Advise.advise_building,
                                        Building.building_name,
                                        Advise.advise_classroom,
                                        Classroom.classroom_name,
                                        Advise.advise_date_request,
                                        Advise.advise_date_start,
                                        Advise.advise_date_ends,
                                        Advise.advise_modality,
                                        Advise.advise_url,
                                        Advise.advise_comments,
                                        Advise.advise_status
                                    }).FirstOrDefaultAsync(code => code.advise_code == id);

                if (advise == null)
                {
                    return NotFound();
                }

                return Ok(advise);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/<AdvisesController>
        [HttpPost]
        public async Task<ActionResult> Post(Advise advise)
        {
            try
            {
                context.Advise.Add(advise);

                await context.SaveChangesAsync();

                return new CreatedAtRouteResult("GetAdvise", new { id = advise.advise_code }, advise);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<AdvisesController>{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Advise advise)
        {
            try
            {
                if (advise == null)
                {
                    return NotFound();
                }
                else if (advise.advise_code == id)
                {
                    context.Entry(advise).State = EntityState.Modified;

                    await context.SaveChangesAsync();

                    return Ok(advise);
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

        // DELETE api/<AdvisesController>/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var advise = await context.Advise.FirstOrDefaultAsync(code => code.advise_code == id);

                if (advise == null)
                {
                    return NotFound();
                }
                else
                {
                    context.Advise.Remove(advise);

                    await context.SaveChangesAsync();

                    return Ok(advise);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
