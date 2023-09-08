using Domain.Aggregates.Clients;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IEmailRepository
    {
        Task Send(string email, string message);
    }
}
