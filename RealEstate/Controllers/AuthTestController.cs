using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Utility;

namespace RealEstate.Controllers
{
    [Route("api/AuthTest")]
    [ApiController]
    public class AuthTestController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<string>> GetSomething()
        {
            return "you are authenticated";
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<ActionResult<string>> GetSomething(int someintvalue)
        {
            return "you are authorized with the role of admin";
        }
    }
}
