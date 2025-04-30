using School.Core.Features.User.Queries.Results;
using School.Data.Entities.IdentityEntities;

namespace School.Core.Mapping.User
{
    public partial class UserProfile
    {
        public void GetUsersPaginatedMapping()
        {
            CreateMap<ApplicationUser, GetUsersPaginatedResponse>()
                .ForMember(dest => dest.Phone,
                   opt => opt.MapFrom(src => src.PhoneNumber));

        }
    }
}


