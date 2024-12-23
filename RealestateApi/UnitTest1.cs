using Microsoft.AspNetCore.Mvc.Testing;
using RealEstate.Models;
using RealEstate.Models.Dto;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace RealestateApi
{
    public class BasicTests : IClassFixture<WebApplicationFactory<Properties>>
    {
       private readonly WebApplicationFactory<Properties> _factory;

        public BasicTests(WebApplicationFactory<Properties> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAllProperties_ReturnOkResult()
        {
            HttpClient client = _factory.CreateClient();

            var response = await client.GetAsync("/api/properties");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetPropertyById_ReturnOkResult()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/properties/property/house-for-sale");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task CreateProperty_WithImage_ReturnsOkResult()
        {
            var client = _factory.CreateClient();

            using var formContent = new MultipartFormDataContent();

            var propertyDto = new CreatePropertyDto
            {
                Title = "House for sale",
                Description = "A beautiful house for sale",
                Address = "1234 Main St",
                City = "San Francisco",
                State = "CA",
                Zip = "94123",
                Year_Built = "2020",
                Bedrooms = 3,
                Bathrooms = 2,
                Prop_Type = "House",
                Prop_Status = "For Sale",
                Lot_Size = 2000,
                Views = 0,
                Agent_Id = "532d1789-c1d9-4d36-bfc1-c2bb4372c7cf",
               
            };

            var json = JsonSerializer.Serialize(propertyDto);
            formContent.Add(new StringContent(json, Encoding.UTF8, "application/json"), "propertyDto");

            var imagePath = "path_to_your_image.jpg"; // Replace with the path to your image
            var imageContent = new ByteArrayContent(await File.ReadAllBytesAsync(imagePath));
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg"); // or the appropriate content type
            formContent.Add(imageContent, "imageFile", Path.GetFileName(imagePath));

            var response = await client.PostAsync("/api/properties", formContent);

            response.EnsureSuccessStatusCode();
        }



        [Fact]
        public async Task CreateProperty_returnBadRwquest()
        {
            var client = _factory.CreateClient();
            var response = await client.PostAsJsonAsync("/api/properties", new { });

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

    }
}