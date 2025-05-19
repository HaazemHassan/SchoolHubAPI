namespace School.Services.ServicesContracts
{
    public interface IEmailService
    {
        public Task<bool> SendEmail(string email, string Message);

    }
}
