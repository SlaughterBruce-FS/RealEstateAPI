namespace RealEstate.Models.Dto
{
    public class UpdateUserPorfileDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public IFormFile? ProfileImage { get; set; }

    }
}
