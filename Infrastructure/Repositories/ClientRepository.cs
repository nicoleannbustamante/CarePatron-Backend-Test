using Core.Exceptions;
using Domain.Aggregates.Clients;
using Infrastructure.EFCore.Data;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories
{
    public class ClientRepository :  IClientRepository
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<ClientRepository> _logger;

        public ClientRepository(DataContext dataContext, ILogger<ClientRepository> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }

        public async Task Create(Client client, CancellationToken cancellationToken)
        {
            try
            {
                await _dataContext.AddAsync(client);
                await _dataContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error adding client to data context: " + ex.Message);
                throw new ClientException("Error adding client to data context: ", ex);
            }
        }

        public async Task Update(Client client, CancellationToken cancellationToken)
        {
            try
            {
                var existingClient = await _dataContext.Clients.FirstOrDefaultAsync(x => x.Id == client.Id);

                if (existingClient == null)
                {
                    throw new Exception("No client exists for the given Id");
                }

                existingClient.Update(client.FirstName, client.LastName, client.Email, client.PhoneNumber);
                await _dataContext.SaveChangesAsync(cancellationToken);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error updating client to data context: " + ex.Message);
                throw new ClientException("Error updating client to data context: ", ex);
            }
        }

        public async Task<IEnumerable<Client>> GetAllClients()
        => await _dataContext.Clients.ToListAsync();

        public async Task<Client> GetClientById(Guid id)
        {
            var client =  await _dataContext.Clients.Where(x => x.Id == id).FirstOrDefaultAsync();
            if(client == null)
            {
                throw new ClientException("No client exists for the given Id");
            }

            return client;
        }

        public async Task<IEnumerable<Client>> SearchClients(string searchValue)
        => await _dataContext.Clients.Where(x => x.FirstName.ToLower() == searchValue.ToLower() || 
                                                 x.LastName.ToLower() == searchValue.ToLower()) .ToListAsync();

        public async Task Delete(Guid id, CancellationToken cancellationToken)
        {
            var client = await _dataContext.Clients.FirstOrDefaultAsync(x => x.Id == id);
            if (client == null)
                throw new ClientException("Client does not exists based from the given Id");

            _dataContext.Clients.Remove(client);
            await _dataContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _dataContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _dataContext.Database.CurrentTransaction.CommitAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _dataContext.Database.CurrentTransaction.RollbackAsync();
        }
    }
}

