using AsesoraApi.Context;
using AsesoraApi.Models;
using AsesoraApi.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
#nullable enable

namespace AsesoraApi.Controllers
{
    [ApiController]
    [Route("/api/users")]
    public class UsersController : Controller
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public UsersController(AppDbContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET api/<UsersController>
        [HttpGet]
        public async Task<ActionResult<List<Userx>>> Get()
        {
            try
            {
                var users = await context.Userx.ToListAsync();

                return Ok(mapper.Map<List<Userx>>(users));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/<UsersController>/{id}
        [HttpGet("{id?}/{email?}", Name = "GetUser")]
        public async Task<ActionResult<Userx>> Get(String? id, String? email)
        {
            try
            {
                var user = await context.Userx.FirstOrDefaultAsync(code => code.userx_code == id ||
                  code.userx_email == email);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(mapper.Map<Userx>(user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult> Post(UserxImage userx)
        {
            try
            {
                var hash = HashPassword.Hash(userx.userx_password);
                userx.userx_password = hash.password;
                userx.userx_salt = hash.Salt;

                if (userx.userx_image.Length > 39300000) // Máximo de 5 MB
                {
                    return BadRequest("La imagen excede el tamaño permitido");
                }

                context.Userx.Add(userx);

                await context.SaveChangesAsync();

                return new CreatedAtRouteResult("GetUser", new { id = userx.userx_code }, userx);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<UsersController>/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(String id, UserxImage userx)
        {
            try
            {
                if (userx == null)
                {
                    return NotFound();
                }
                else if (userx.userx_code == id)
                {
                    var hash = HashPassword.Hash(userx.userx_password);
                    userx.userx_password = hash.password;
                    userx.userx_salt = hash.Salt;

                    if (userx.userx_image.Length > 39300000) // Máximo de 5MB
                    {
                        return BadRequest("La imagen excede el tamaño permitido");
                    }

                    context.Entry(userx).State = EntityState.Modified;

                    await context.SaveChangesAsync();

                    return Ok(userx);
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

        // DELETE api/<UsersController>/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(String id)
        {
            try
            {
                var user = await context.Userx.FirstOrDefaultAsync(code => code.userx_code == id);

                if (user == null)
                {
                    return NotFound();
                }
                else
                {
                    context.Userx.Remove(user);

                    await context.SaveChangesAsync();

                    return Ok(user);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
