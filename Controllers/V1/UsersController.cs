using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using RestApi.DataAccess;
using RestApi.Dtos;
using RestApi.Entities;
using RestApi.Entities.DataEntities;
using RestApi.Services;
using RestApi.Utils;

namespace RestApi.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UniversityDBContext _context;
        private readonly IStringLocalizer<SharedResource> _sharedResourceLocalizer;

        public UsersController(UniversityDBContext context, IStringLocalizer<SharedResource> sharedResourceLocalizer)
        {
            _context = context;
            _sharedResourceLocalizer = sharedResourceLocalizer;
        }

        [HttpGet]
        [Route("SharedResource")]
        public IActionResult GetUsingSharedResource()
        {
            var welcome = _sharedResourceLocalizer.GetString("Welcome").Value ?? string.Empty;
            var todayIs = string.Format(_sharedResourceLocalizer.GetString("TodayIs").Value, DateTime.Now.ToLongDateString());

            return Ok(new
            {
                Welcome = welcome,
                TodayIs = todayIs
            });
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            List<UserDto> usuarios = new();

            var userDto = new UserDto();

            var users = await _context.Users.ToListAsync();

            users.ForEach((u) =>
            {
                var userDto = new User();
                userDto = u;

                usuarios.Add(userDto);
            });

            return Ok(usuarios);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult<User>> GetUser(int id) //En este caso ActionResult<TipoDato> se especifica cuando quiero retornar una entidad con ActionResult solo no funcionaria
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) // Error de tipo concurrencia por ejemplo 2 usuarios intentando actualizar a la vez
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
