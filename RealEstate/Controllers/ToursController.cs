using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models;
using RealEstate.Models.Dto;
using RealEstate.Services;
using System.Net;

namespace RealEstate.Controllers
{
    [Route("api/tours")]
    [ApiController]
    public class ToursController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private ApiResponse _response;

        public ToursController(ApplicationDbContext db)
        {
            _db = db;
            _response = new ApiResponse();
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetTours(int page = 1, int pageSize = 25, DateTime? tourDate = null)
        {
            try
            {
                
                    IQueryable<Tours> toursQuery = _db.Tours
                        .Include(u => u.User)
                        .Include(p => p.Properties);

                    if (tourDate.HasValue)
                    {
                        toursQuery = toursQuery.Where(t => t.Tour_Date == DateOnly.FromDateTime(tourDate.Value));
                    }

                    var totalCount = toursQuery.Count();
                    var totalPages = 0;

                    if (totalCount > 0)
                    {
                        totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
                    }

                    var tours = toursQuery.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                    if (tours == null || tours.Count == 0)
                    {
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.IsSuccess = false;
                        return BadRequest(_response);
                    }

                    _response.Result = tours;
                    _response.IsSuccess = true;
                    _response.StatusCode = HttpStatusCode.OK;
                    _response.Page = page;
                    _response.TotalPages = totalPages;
                   }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                return BadRequest(_response);
            }

            return Ok(_response);
        }

        [HttpGet("{id}", Name ="GetTour")]
        public async Task<ActionResult<ApiResponse>> GetTour(int id)
        {
            if(id == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

            Tours tour = _db.Tours.FirstOrDefault(t => t.Id == id);

            if(tour == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

            _response.Result = tour;
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpGet("agent/{agentId}")]
        public async Task<ActionResult<ApiResponse>> GetMyTours(string agentId)
        {
            if (String.IsNullOrEmpty(agentId))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

            var tours = _db.Tours.Where(t => t.AgentId == agentId);

            if (tours == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

            _response.Result = tours;
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }



        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateTour([FromBody]ToursCreateDto toursCreateDto)
        {
            try
            {

                Tours tourToCreate = new()
                {
                    Name = toursCreateDto.Name,
                    AgentId = toursCreateDto.AgentId,
                    Phone_Number = toursCreateDto.Phone_Number,
                    PropertyId = toursCreateDto.PropertyId,
                    Email = toursCreateDto.Email,
                    Time = toursCreateDto.Time,
                    Tour_Date = toursCreateDto.Tour_Date

                };
                _db.Tours.Add(tourToCreate);
                await _db.SaveChangesAsync();
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.Created;
                _response.Result = toursCreateDto;
                return CreatedAtRoute("GetTour", new { id = tourToCreate.Id }, _response);

            } catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                return BadRequest(_response);
            }
          
        }

    }
}
