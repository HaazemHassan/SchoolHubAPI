using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using School.Data.Entities.IdentityEntities;
using School.Data.Helpers;
using School.Infrastructure.RepositoriesContracts;
using School.Services.ServicesContracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace School.Services.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRefreshTokenRepository _refreshTokenRepository;



        public AuthenticationService(JwtSettings jwtSettings, UserManager<ApplicationUser> userManager, IRefreshTokenRepository refreshTokenRepository)
        {
            _jwtSettings = jwtSettings;
            _userManager = userManager;
            _refreshTokenRepository = refreshTokenRepository;
        }


        public async Task<JwtResult> GenerateJwtAsync(ApplicationUser user)
        {
            var accessToken = new JwtSecurityToken(
                 issuer: _jwtSettings.Issuer,
                 audience: _jwtSettings.Audience,
                 claims: await GetUserClaims(user),
                 signingCredentials: GetSigningCredentials(),
                 expires: DateTime.UtcNow.AddDays(_jwtSettings.AccessTokenExpirationMinutes)

             );

            var refreshToken = GenerateRefreshToken(user.UserName);
            await AddRefreshTokenToDatabase(refreshToken.Token, accessToken.Id, user.Id);

            JwtResult jwtResult = new JwtResult
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                RefreshToken = refreshToken
            };
            return jwtResult;
        }

        #region Helper functions
        private async Task<List<Claim>> GetUserClaims(ApplicationUser user)
        {
            var claims = new List<Claim>()
             {
                 new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                 new Claim(ClaimTypes.Name,user.UserName),

                 //to make unique token every time
                 new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())

             };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            return claims;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret));
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        }

        private RefreshTokenDTO GenerateRefreshToken(string username)
        {
            var randomBytes = new byte[64];
            RandomNumberGenerator.Fill(randomBytes);
            string refreshTokenValue = Convert.ToBase64String(randomBytes);

            return new RefreshTokenDTO
            {
                Token = refreshTokenValue,
                ExpirationDate = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays),
                CreatedAt = DateTime.UtcNow,
                Username = username


            };
        }

        private async Task AddRefreshTokenToDatabase(string value, string accessTokenJti, int userId)
        {
            var refreshToken = new RefreshToken
            {
                Created = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays),
                AccessTokenJTI = accessTokenJti,
                Token = value,
                UserId = userId
            };
            await _refreshTokenRepository.AddAsync(refreshToken);
        }


        #endregion

    }
}

