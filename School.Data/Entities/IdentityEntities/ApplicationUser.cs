using Microsoft.AspNetCore.Identity;

namespace School.Data.Entities.IdentityEntities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string? Address { get; set; }
        public string? Country { get; set; }

    }
}
