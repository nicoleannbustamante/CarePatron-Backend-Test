namespace Application.Services.Interfaces
{
    public interface IEmailService
    {
        Task Send(string email, string message);
    }
}