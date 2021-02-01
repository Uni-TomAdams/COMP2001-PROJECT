using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using COMP2001_Authentication_Application.Data;
using COMP2001_Authentication_Application.Models;

namespace COMP2001_Authentication_Application.Controllers.API
{
    [Route("tadams/auth/api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataAccess _context;

        public UsersController(DataAccess context)
        {
            _context = context;
        }

        // POST api/user
        [HttpPost]
        public async Task<ActionResult<UserModel>> Register([FromBody] UserModel user)
        {
            string response;
            _context.Register(user, out response);

            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(208);
            }
        }
    }
}
