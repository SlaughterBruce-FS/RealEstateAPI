﻿
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
    [Route("api/properties")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IBlobService _blobService;
        private ApiResponse _response;

        public PropertiesController(ApplicationDbContext db, IBlobService blobService)
        {
            _db = db;
            _response = new ApiResponse();
            _blobService = blobService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetProperties(int page = 1, int pageSize = 25)
        {
            try
            {
                var properties = _db.Properties
                    .Include(u => u.User);

                var totalCount = properties.Count();
                var totalPages = 0;

                if (totalCount > 0)
                {
                    totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
                }

                if (properties == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                _response.Result = properties.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                _response.Page = page;
                _response.TotalPages = totalPages;
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

            return Ok(_response);
        }


        [HttpGet("agent/{agentId}", Name = "GetMyProperties")]
        public async Task<ActionResult<ApiResponse>> GetMyProperties(string? agentId)
        {
            if (String.IsNullOrEmpty(agentId))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

            var properties = _db.Properties
                .Where(p => p.Agent_Id == agentId)
                .Include(u=>u.User);

            if (properties == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            else
            {
                _response.Result = properties;
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }


        }

        [HttpGet("property/{slug}", Name = "GetMyProperty")]
        public async Task<ActionResult<ApiResponse>> GetMyProperty(string slug)
        {
            if (String.IsNullOrEmpty(slug))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

            Properties properties = await _db.Properties.Include(u=> u.User).FirstOrDefaultAsync(p => p.Slug == slug);

            if (properties == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }




            _response.Result = properties;
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);



        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateProperty([FromForm] CreatePropertyDto createProprtyDto)
        {
            try
            {
                if (ModelState.IsValid) {
                    if (createProprtyDto.File == null || createProprtyDto.File.Length == 0)
                    {
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.IsSuccess = false;
                        return BadRequest();
                    }
                    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(createProprtyDto.File.FileName)}";

                    Properties newProperty = new()
                    {
                        Title = createProprtyDto.Title,
                        Description = createProprtyDto.Description,
                        Address = createProprtyDto.Address,
                        City = createProprtyDto.City,
                        State = createProprtyDto.State,
                        Zip = createProprtyDto.Zip,
                        Price = createProprtyDto.Price,
                        Agent_Id = createProprtyDto.Agent_Id,
                        Is_Published = createProprtyDto.Is_Published,
                        Is_Rent = createProprtyDto.Is_Rent,
                        //Views = createProprtyDto.Views,
                        //Slug = createProprtyDto.Slug,
                        Slug = createProprtyDto.Address.Trim().ToLower().Replace(" ", "-") +"-" + createProprtyDto.Zip.Trim().ToLower().Replace(" ", "-"),
                        Prop_Type = createProprtyDto.Prop_Type,
                        Prop_Status = createProprtyDto.Prop_Status,
                        Bedrooms = createProprtyDto.Bedrooms,
                        Bathrooms = createProprtyDto.Bathrooms,
                        Garages = createProprtyDto.Garages,
                        Area = createProprtyDto.Area,
                        Lot_Size = createProprtyDto.Lot_Size,
                        Year_Built = createProprtyDto.Year_Built,
                        Date_listed = DateTime.Now,
                        Featured_Image = await _blobService.UploadBlob(fileName, SD.SD_STORAGE_CONTAINER, createProprtyDto.File)
                    };
                    _db.Properties.Add(newProperty);
                    _db.SaveChanges();
                    _response.Result = createProprtyDto;
                    _response.StatusCode = HttpStatusCode.Created;
                    return CreatedAtRoute("GetMyProperty", new { slug = newProperty.Slug }, _response);
                } else
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


        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse>> UpdateProperty(int id , [FromForm]UpdatePropertyDto updatePropertyDto )
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //if (updatePropertyDto == null || id != updatePropertyDto.Id )
                    //{
                    //    _response.StatusCode = HttpStatusCode.BadRequest;
                    //    _response.IsSuccess = false;
                    //    return BadRequest();
                    //}
                    //string fileName = $"{Guid.NewGuid()}{Path.GetExtension(createProprtyDto.File.FileName)}";

                    Properties propertyFromdb = await _db.Properties.FindAsync(id);

                    if (propertyFromdb == null)
                    {
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.IsSuccess = false;
                        return BadRequest();
                    }

                    propertyFromdb.Title = updatePropertyDto.Title;
                    propertyFromdb.Description = updatePropertyDto.Description;
                    propertyFromdb.Address = updatePropertyDto.Address;
                    propertyFromdb.City = updatePropertyDto.City;
                    propertyFromdb.State = updatePropertyDto.State;
                    propertyFromdb.Zip = updatePropertyDto.Zip;
                    propertyFromdb.Price = updatePropertyDto.Price;
                    propertyFromdb.Is_Published = updatePropertyDto.Is_Published;
                    propertyFromdb.Is_Rent = updatePropertyDto.Is_Rent;
                    propertyFromdb.Agent_Id = updatePropertyDto.Agent_Id;
                    propertyFromdb.Slug = updatePropertyDto.Address.Trim().ToLower().Replace(" ", "-") +"-"+ updatePropertyDto.Zip.Trim().ToLower().Replace(" ", "-");
                    propertyFromdb.Prop_Type = updatePropertyDto.Prop_Type;
                    propertyFromdb.Prop_Status = updatePropertyDto.Prop_Status;
                    propertyFromdb.Bedrooms = updatePropertyDto.Bedrooms;
                    propertyFromdb.Bathrooms = updatePropertyDto.Bathrooms;
                    propertyFromdb.Garages = updatePropertyDto.Garages;
                    propertyFromdb.Area = updatePropertyDto.Area;
                    propertyFromdb.Lot_Size = updatePropertyDto.Lot_Size;
                    propertyFromdb.Year_Built = updatePropertyDto.Year_Built;

                    if (updatePropertyDto.File != null && updatePropertyDto.File.Length > 0)
                    {
                        string fileName = $"{Guid.NewGuid()}{Path.GetExtension(updatePropertyDto.File.FileName)}";
                        await _blobService.DeleteBlob(propertyFromdb.Featured_Image.Split('/').Last(), SD.SD_STORAGE_CONTAINER);

                        propertyFromdb.Featured_Image = await _blobService.UploadBlob(fileName, SD.SD_STORAGE_CONTAINER, updatePropertyDto.File);

                    }

                    _db.Properties.Update(propertyFromdb);
                    _db.SaveChanges();
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

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteProperty(int id)
        {
            try
            {
                if(id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest();
                }

                Properties propertyFromDb = await _db.Properties.FindAsync(id);

                if(propertyFromDb == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest();
                }

                await _blobService.DeleteBlob(propertyFromDb.Featured_Image.Split('/').Last(), SD.SD_STORAGE_CONTAINER);

                int miloseconds = 2000;
                Thread.Sleep(miloseconds);

                _db.Properties.Remove(propertyFromDb);
                _db.SaveChanges();
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);


            } catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;

        }


        [HttpPut("views/{id}")]
        public async Task<ActionResult<ApiResponse>> UpdatePropertyViews(int id,  UpdatePropertyViews updatePropertyViews)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (updatePropertyViews == null || id != updatePropertyViews.Id)
                    {
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.IsSuccess = false;
                        return BadRequest(_response);
                    }

                    Properties propertyFromDb = await _db.Properties.FindAsync(id);

                    if (propertyFromDb == null)
                    {
                        _response.StatusCode = HttpStatusCode.NotFound;
                        _response.IsSuccess = false;
                        return NotFound(_response);
                    }

                    // Increment the views count
                    if(propertyFromDb.Views == null || propertyFromDb.Views == 0)
                    {
                        propertyFromDb.Views = 1;
                    } else
                    {
                        propertyFromDb.Views++;
                    }
                   

                    await _db.SaveChangesAsync();  // Use async save for better performance

                    _response.StatusCode = HttpStatusCode.NoContent; // You might want to return a 204 No Content
                    return Ok(_response);
                }
                else
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

    }


}  
