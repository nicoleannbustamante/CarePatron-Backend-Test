using Domain.Aggregates.Clients;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IClientRepository
    {
        Task Create(Client client, CancellationToken cancellationToken);
        Task Update(Client client, CancellationToken cancellationToken);
        Task Delete(Guid id, CancellationToken cancellationToken);
        Task<Client> GetClientById(Guid id);
        Task<IEnumerable<Client>> GetAllClients();
        Task<IEnumerable<Client>> SearchClients(string searchValue);
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
