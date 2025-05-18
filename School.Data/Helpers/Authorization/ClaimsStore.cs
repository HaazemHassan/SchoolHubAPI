using System.Security.Claims;

namespace School.Data.Helpers.Authorization
{
    public static class ClaimsStore
    {
        public static List<Claim> Claims = new List<Claim>
        {
            new Claim("Add student" , "false"),
            new Claim("Edit student" , "false"),
            new Claim("Delete student" , "false"),
        };
    }
}
