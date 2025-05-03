using Microsoft.EntityFrameworkCore;
using School.Data.Entities.IdentityEntities;
using School.Infrastructure.Context;
using School.Infrastructure.InfrastructureBases;
using School.Infrastructure.RepositoriesContracts;

namespace School.Infrastructure.Repositories
{
    public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
    {

        private readonly DbSet<RefreshToken> _refreshTokens;


        public RefreshTokenRepository(AppDbContext context) : base(context)
        {
            _refreshTokens = context.Set<RefreshToken>();
        }

    }
}
