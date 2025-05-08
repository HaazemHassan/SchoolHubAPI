using Microsoft.AspNetCore.Identity;

namespace School.Data.Entities.IdentityEntities
{
    public class ApplicationRole : IdentityRole<int>
    {
        public ApplicationRole()
        {

        }
        public ApplicationRole(string role)
        {
            Name = role;
        }
    }
}
