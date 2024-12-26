using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models;
using RealEstate.Models.Dto;
using System.Net;

namespace RealEstate.Controllers
{
    [Route("/api/users")]
    [ApiController]
    public class UsersController : Controller
    {

        private readonly ApplicationDbContext _db;
        private ApiResponse _response;

        public UsersController(ApplicationDbContext db)
        {
            _db = db;
            _response = new ApiResponse();
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<ApiResponse>> GetUser(string userId)
        {
            if (String.IsNullOrEmpty(userId))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

            var user = await _db.UserProfiles.FirstOrDefaultAsync(p => p.UserId == userId);

            if (user == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }




            _response.Result = user;
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);

     
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetAgents()
        {
            try
            {
                var users = _db.UserProfiles;

                if (users == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

             

                _response.Result = users;
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

            return Ok(_response);
        }


    
    }
    
}
