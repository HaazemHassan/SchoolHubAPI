using School.Data.Helpers.Authorization;

namespace School.Core.Features.Authorization.Queries.Results
{
    public class GetUserClaimsResponse
    {
        public int UserId { get; set; }
        public List<UserClaim> Claims { get; set; } = new List<UserClaim>();
    }
}
