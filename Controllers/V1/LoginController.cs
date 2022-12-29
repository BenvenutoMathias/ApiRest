using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using RestApi.DataAccess;
using RestApi.Entities.DataEntities;
using RestApi.Services;
using RestApi.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace RestApi.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UniversityDBContext _context;
        private readonly JwtSettings _jwtSettings;
        private readonly IGeneralServices _services;

        public LoginController(UniversityDBContext context, JwtSettings jwtSettings, IGeneralServices services)
        {
            _context = context;
            _jwtSettings = jwtSettings;
            _services = services;
        }

        [HttpPost]
        public async Task<IActionResult> GetToken(UserLogins userLogin)
        {
            try
            {
                var token = new UserTokens();
                //Completar aca
                var users = await _context.Users.ToListAsync();

                var user = users.FirstOrDefault(user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase) && user.Password.Equals(userLogin.Password, StringComparison.OrdinalIgnoreCase));

                if (user is not null)
                {
                    token = JwtHelpers.GenTokenKey(new User()
                    {
                        Name = user.Name,
                        EmailAdress = user.EmailAdress,
                        Id = user.Id,
                        Role = user.Role
                    }, _jwtSettings);
                }
                else
                {
                    return BadRequest("Wrong Password");
                }

                return Ok(token);
            }
            catch (Exception ex)
            {
                throw new Exception("GetToken Error", ex);
            }
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> GetUserList()
        {
            return Ok(await _services.GetUsers());
        }
    }
}