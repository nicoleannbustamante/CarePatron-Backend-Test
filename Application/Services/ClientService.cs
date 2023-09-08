using Application.Models.Responses;
using Application.Services.Interfaces;
using AutoMapper;
using Core.Exceptions;
using Domain.Aggregates.Clients;
using Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class ClientService : IClientService
    {

        private readonly IClientRepository _clientRepository;
        private readonly IEmailService _emailService;
        private readonly IDocumentService _documentService;
        private readonly IMapper _mapper;
        private readonly ILogger<ClientService> _logger;       

        public ClientService(IClientRepository clientRepository, 
                             IMapper mapper,
                             IEmailService emailService,
                             IDocumentService documentService,
                             ILogger<ClientService> logger)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
            _emailService = emailService;
            _documentService = documentService;
            _logger = logger;
        }

        public async Task CreateClient(Client client, CancellationToken cancellationToken)
        {
            //NOTE: (1) Handling Data Integrity : using Transactions
            //using (var transaction = await _clientRepository.BeginTransactionAsync())
            //{
                try
                {
                    await _clientRepository.Create(client, cancellationToken);

                    await _emailService.Send(client.Email, "Hi there - welcome to my Carepatron portal.");

                    await _documentService.SyncDocumentsFromExternalSource(client.Email);

                    //transaction.Commit();
                }
                catch (Exception ex)
                {
                //transaction.Rollback();

                //NOTE: (2) Handling Data Integrity in case of failure: using Compensating actions
                if (!(ex is ClientException))
                    {
                        await _clientRepository.Delete(client.Id, cancellationToken);
                    }

                    _logger.LogError("Error occurred while creating client: " + ex.Message);
                    throw new ClientException("Error occurred while creating client: ", ex);
                }
            //}                   
        }

        public async Task UpdateClient(Client oldClient, Client updateClient, CancellationToken cancellationToken)
        {
            try
            {
                await _clientRepository.Update(updateClient, cancellationToken);
                if (oldClient.Email != updateClient.Email)
                {
                    await _emailService.Send(updateClient.Email, "Hi there - welcome to my Carepatron portal.");
                    await _documentService.SyncDocumentsFromExternalSource(updateClient.Email);
                }
            }
            catch (Exception ex)
            {
                //NOTE: Handling Data Integrity in case of failure: using Compensating actions
                if (!(ex is ClientException))
                {
                    await _clientRepository.Update(oldClient, cancellationToken);
                }

                _logger.LogError("Error occurred while updating client: " + ex.Message);
                throw new ClientException("Error occurred while updating client: ", ex);
            }
          
        }
        
        public async Task<Client> GetClientById(Guid id) => await _clientRepository.GetClientById(id);

        public async Task<IEnumerable<ClientResponse>> GetAllClients()
        {
            var clients = await _clientRepository.GetAllClients();
            return _mapper.Map<IEnumerable<ClientResponse>>(clients);
        }

        public async Task<IEnumerable<ClientResponse>> SearchClients(string searchValue)
        {
            var clients = await _clientRepository.SearchClients(searchValue);
            return _mapper.Map<IEnumerable<ClientResponse>>(clients);
        }

    }

}
