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
    [Route("tadams/auth/api/user")]
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

        // POST api/user
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UserModel>> Post([FromBody] UserModel user)
        {
            string Response;
            Register(user, out Response);

            if (Response != null)
            {
                return StatusCode(200, Response);
            }
            else
            {
                return StatusCode(208, "Email already exists.");
            }
        }

        // GET api/user
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<UserModel>> Get([FromBody] UserModel User)
        {
            // Attempt to validate a users credentials with the database
            bool verificationStatus = GetValidation(User);

            // Fake claims, would typically need to include users role for instance.
            Claim[] claims = new[]
            {
                    new Claim(ClaimTypes.Name, User.Email),
            };

            // Generate JWT token
            var jwtResult = _jwtManager.GenerateTokens(claims, DateTime.Now);
            return Ok(new LoginResultant()
            {
                AccessToken = verificationStatus
                              ? jwtResult.AccessToken
                              : "",
                Verified = verificationStatus
                              ? "true"
                              : "false"
            });
        }

        // PUT api/user/{id}
        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<UserModel>> Update([FromBody] UserModel user, int id)
        {
            _context.UpdateUser(user, id);

            return Ok();
        }

        // DELETE api/user/{id}
        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<UserModel>> Delete(int id)
        {
            _context.DeleteUser(id);

            return Ok();
        }

        private bool GetValidation(UserModel User)
        {
            return _context.ValidateUser(User);
        }

        private void Register(UserModel User, out string Response)
        {
            _context.Register(User, out Response);
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