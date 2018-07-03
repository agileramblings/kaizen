using Microsoft.AspNetCore.Identity;

namespace kaizen.api.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the kaizenapiUser class
    public class kaizenapiUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string Locale { get; set; }
        public string PictureUrl { get; set; }
        public string CardId { get; set; }
    }
}