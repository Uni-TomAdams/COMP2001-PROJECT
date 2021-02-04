using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COMP2001_Authentication_Application.Data;
using COMP2001_Authentication_Application.Models;
using COMP2001_Authentication_Application.Managers;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace COMP2001_Authentication_Application.Controllers.API
{
    [Route("tadams/auth/api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly DataAccess _context;
        private readonly IJWTManager _jwtManager;

        public UsersController(DataAccess context, IJWTManager jWTManager)
        {
            _context = context;
            _jwtManager = jWTManager;
        }

        // POST api/users
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UserModel>> Register([FromBody] UserModel user)
        {
            string response;
            _context.Register(user, out response);

            if (response != null)
            {
                return StatusCode(200, response);
            }
            else
            {
                return StatusCode(208, "Email already exists.");
            }
        }

        // GET api/users
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<UserModel>> LoginUser([FromBody] UserModel user)
        {
            // Attempt to validate a users credentials with the database
            int verificationStatus = _context.ValidateUser(user);

            // Fake claims, would typically need to include users role for instance.
            Claim[] claims = new[]
            {
                    new Claim(ClaimTypes.Name, user.Email),
            };

            // Generate JWT token
            var jwtResult = _jwtManager.GenerateTokens(claims, DateTime.Now);
            return Ok(new LoginResultant()
            {
                AccessToken = verificationStatus == 1
                              ? jwtResult.AccessToken
                              : "",
                Verified = verificationStatus == 1
                              ? "true"
                              : "false"
            });
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<UserModel>> UpdateUser([FromBody] UserModel user)
        {
            _context.UpdateUser(user, user.UserID);

            return Ok();
        }

        public class LoginResultant
        {
            [Required]
            [JsonPropertyName("AccessToken")]
            public string AccessToken { get; set; }

            [Required]
            [JsonPropertyName("Verified")]
            public string Verified { get; set; }
        }
    }
}