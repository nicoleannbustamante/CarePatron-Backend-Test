using Application.Models.Responses;
using Domain.Aggregates.Clients;

namespace Application.Services.Interfaces
{
    public interface IClientService
    {
        Task CreateClient(Client client, CancellationToken cancellationToken);
        Task UpdateClient(Client oldClient, Client updateClient, CancellationToken cancellationToken);
        Task<Client> GetClientById(Guid id);
        Task<IEnumerable<ClientResponse>> GetAllClients();
        Task<IEnumerable<ClientResponse>> SearchClients(string searchValue);
       
    }
}
