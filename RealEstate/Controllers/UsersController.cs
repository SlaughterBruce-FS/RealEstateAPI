using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models;
using RealEstate.Models.Dto;
using RealEstate.Services;
using RealEstate.Utility;
using System.Net;

namespace RealEstate.Controllers
{
    [Route("/api/users")]
    [ApiController]
    public class UsersController : Controller
    {

        private readonly ApplicationDbContext _db;
        private ApiResponse _response;
        private readonly IBlobService _blobService;

        public UsersController(ApplicationDbContext db, IBlobService blobService)
        {
            _db = db;
            _response = new ApiResponse();
            _blobService = blobService;
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
                var users = _db.UserProfiles.Where(u => u.Role == "agent");

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


        [HttpPut("{userId}")]
        public async Task<ActionResult<ApiResponse>> UpdateProfilety(string userId, [FromForm] UpdateUserPorfileDto updateUserPorfileDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //if (updateUserPorfileDto == null || userId != null)
                    //{
                    //    _response.StatusCode = HttpStatusCode.BadRequest;
                    //    _response.IsSuccess = false;
                    //    return BadRequest();
                    //}

                    UserProfiles userFromdb = await _db.UserProfiles.FindAsync(userId);


                
                    if (userFromdb == null)
                    {
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.IsSuccess = false;
                        return BadRequest();
                    }

                    userFromdb.Phone = updateUserPorfileDto.Phone;
                    userFromdb.Email = updateUserPorfileDto.Email;
                    userFromdb.FirstName = updateUserPorfileDto.FirstName;
                    userFromdb.LastName = updateUserPorfileDto.LastName;

                    if (updateUserPorfileDto.ProfileImage != null && updateUserPorfileDto.ProfileImage.Length > 0 && userFromdb.Profile_Image != null)
                    {


                        string fileName = $"{Guid.NewGuid()}{Path.GetExtension(updateUserPorfileDto.ProfileImage.FileName)}";
                        await _blobService.DeleteBlob(userFromdb.Profile_Image.Split('/').Last(), SD.SD_STORAGE_CONTAINER);

                        userFromdb.Profile_Image = await _blobService.UploadBlob(fileName, SD.SD_STORAGE_CONTAINER, updateUserPorfileDto.ProfileImage);
                    }


                    if (userFromdb.Profile_Image == null && updateUserPorfileDto.ProfileImage != null)
                    {
                        string fileName = $"{Guid.NewGuid()}{Path.GetExtension(updateUserPorfileDto.ProfileImage.FileName)}";

                        userFromdb.Profile_Image = await _blobService.UploadBlob(fileName, SD.SD_STORAGE_CONTAINER, updateUserPorfileDto.ProfileImage);

                    }

                 



                    _db.UserProfiles.Update(userFromdb);
                    _db.SaveChanges();
                    _response.Result = userFromdb;
                    _response.StatusCode = HttpStatusCode.NoContent;
                    return Ok(_response);

                }
                else
                {
                    _response.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                return BadRequest(_response);
            }

            return _response;
        }


    }


    
}
