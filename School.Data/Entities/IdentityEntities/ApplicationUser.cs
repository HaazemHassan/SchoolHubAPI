using Microsoft.AspNetCore.Identity;

namespace School.Data.Entities.IdentityEntities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public ApplicationUser()
        {
            RefreshTokens = new HashSet<RefreshToken>();
        }

        public string FullName { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }

        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }

    }
}
