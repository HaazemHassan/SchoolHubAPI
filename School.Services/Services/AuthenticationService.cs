using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using School.Data.Entities.IdentityEntities;
using School.Data.Helpers.Authentication;
using School.Infrastructure.RepositoriesContracts;
using School.Services.Bases;
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
        private readonly IApplicationUserService _applicationUserService;



        public AuthenticationService(JwtSettings jwtSettings, UserManager<ApplicationUser> userManager, IRefreshTokenRepository refreshTokenRepository, IApplicationUserService applicationUserService)
        {
            _jwtSettings = jwtSettings;
            _userManager = userManager;
            _refreshTokenRepository = refreshTokenRepository;
            _applicationUserService = applicationUserService;
        }


        public async Task<JwtResult?> AuthenticateAsync(ApplicationUser user, DateTime? refreshTokenExpDate)
        {
            if (user is null)
                return null;

            var jwtSecurityToken = await GenerateAccessToken(user);
            var refreshToken = GenerateRefreshToken(user.Id, refreshTokenExpDate);
            await AddRefreshTokenToDatabase(refreshToken, jwtSecurityToken.Id);

            JwtResult jwtResult = new JwtResult
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                RefreshToken = refreshToken
            };
            return jwtResult;
        }

        public async Task<JwtResult?> ReAuthenticateAsync(string refreshToken, string accessToken)
        {
            //Validate token
            var principal = GetPrincipalFromAcessToken(accessToken);
            if (principal is null)
                throw new SecurityTokenException();

            //Read Token To get Cliams
            var jwt = ReadJWT(accessToken);
            if (jwt is null)
                throw new SecurityTokenException("Can't read this token");


            //Get User from calims
            var userId = jwt.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                throw new Exception("User id is null");

            //check if user still exists in db
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                throw new SecurityTokenException("User Is Not Found");

            var currentRefreshToken = await _refreshTokenRepository.GetTableNoTracking()
                                             .FirstOrDefaultAsync(x => x.AccessTokenJTI == jwt.Id &&
                                                                     x.Token == refreshToken &&
                                                                     x.UserId == int.Parse(userId));

            if (currentRefreshToken is null || !currentRefreshToken.IsActive)
                throw new SecurityTokenException("Refresh Token is not valid");

            //new jwt result
            var jwtResult = await AuthenticateAsync(user, currentRefreshToken.Expires);
            if (jwtResult is null)
                return null;
            currentRefreshToken.ReplacedByToken = jwtResult.RefreshToken.Token;
            currentRefreshToken.RevokationDate = DateTime.UtcNow;
            await _refreshTokenRepository.UpdateAsync(currentRefreshToken);

            return jwtResult;

        }

        public ClaimsPrincipal? GetPrincipalFromAcessToken(string token, bool validateLifetime = true)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                ValidateLifetime = validateLifetime
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

                if (!(securityToken is JwtSecurityToken jwtSecurityToken) ||
                    !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    throw new SecurityTokenException("Invalid token");

                return principal;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ServiceOpertaionResult> ConfirmEmailAsync(int userId, string code)
        {
            if (code is null)
                return ServiceOpertaionResult.Failed;

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
                return ServiceOpertaionResult.NotExist;

            if (user.EmailConfirmed)
                return ServiceOpertaionResult.Failed;

            var confirmEmail = await _userManager.ConfirmEmailAsync(user, code);
            return confirmEmail.Succeeded ? ServiceOpertaionResult.Succeeded : ServiceOpertaionResult.Failed;

        }

        #region Helper functions
        private async Task<JwtSecurityToken> GenerateAccessToken(ApplicationUser user)
        {
            return new JwtSecurityToken(
                  issuer: _jwtSettings.Issuer,
                  audience: _jwtSettings.Audience,
                  claims: await GetUserClaims(user),
                  signingCredentials: GetSigningCredentials(),
                  expires: DateTime.UtcNow.AddDays(_jwtSettings.AccessTokenExpirationMinutes)
              );
        }
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

            var customClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(customClaims);

            return claims;
        }
        private SigningCredentials GetSigningCredentials()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret));
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        }
        private RefreshTokenDTO GenerateRefreshToken(int userId, DateTime? expirationDate = null)
        {
            var randomBytes = new byte[64];
            RandomNumberGenerator.Fill(randomBytes);
            string refreshTokenValue = Convert.ToBase64String(randomBytes);

            return new RefreshTokenDTO
            {
                Token = refreshTokenValue,
                ExpirationDate = expirationDate ?? DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays),
                CreatedAt = DateTime.UtcNow,
                UserId = userId
            };


        }
        private async Task AddRefreshTokenToDatabase(RefreshTokenDTO refreshTokenDTO, string accessTokenJti)
        {
            var refreshToken = new RefreshToken
            {
                Created = DateTime.UtcNow,
                Expires = refreshTokenDTO.ExpirationDate,
                AccessTokenJTI = accessTokenJti,
                Token = refreshTokenDTO.Token,
                UserId = refreshTokenDTO.UserId
            };
            await _refreshTokenRepository.AddAsync(refreshToken);
        }
        private JwtSecurityToken ReadJWT(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException(nameof(accessToken));
            }
            var handler = new JwtSecurityTokenHandler();
            var response = handler.ReadJwtToken(accessToken);
            return response;
        }



        #endregion

    }
}

