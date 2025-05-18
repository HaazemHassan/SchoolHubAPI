namespace School.Data.Helpers.Authentication
{
    public class JwtResult
    {
        public string AccessToken { get; set; }
        public RefreshTokenDTO? RefreshToken { get; set; }
    }

    public class RefreshTokenDTO
    {
        public string Token { get; set; }
        public int UserId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
