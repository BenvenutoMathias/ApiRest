using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using RestApi.DataAccess;
using RestApi.Entities.DataEntities;
using RestApi.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace RestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UniversityDBContext _context;
        private readonly JwtSettings _jwtSettings;


        public LoginController(UniversityDBContext context, JwtSettings jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings;
        }

        private IEnumerable<User> Logins = new List<User>
        {
            new User
            {
                Id = 1,
                EmailAdress = "mathiasdvt7@gmail.com",
                Name = "Admin",
                Password = "admin"
            },
            new User
            {
                Id = 2,
                EmailAdress = "pepedvt7@gmail.com",
                Name = "User1",
                Password = "pepe"
            }
        };

        [HttpPost]
        public async Task<IActionResult> GetToken(UserLogins userLogin)
        {
            try
            {
                var token = new UserTokens();
                //Completar aca
                //await _context.Users.ToListAsync();

                //var user = users.First(user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase) && user.Password.Equals(userLogin.Password, StringComparison.OrdinalIgnoreCase));

                //if (user is not null)
                //{
                //    token = JwtHelpers.GenTokenKey(new User()
                //    {
                //        Name = user.Name,
                //        EmailAdress = user.EmailAdress,
                //        Id = user.Id,
                //        Role = user.Role
                //    }, _jwtSettings);
                //}
                //else
                //{
                //    return BadRequest("Wrong Password");
                //}

                return Ok(token);
            }
            catch (Exception ex)
            {
                throw new Exception("GetToken Error", ex);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> GetUserList()
        {
            return Ok(/*await _context.Users.ToListAsync()*/Logins);
        }
    }
}