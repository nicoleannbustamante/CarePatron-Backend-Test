using Application.Models.Requests;
using Application.Services.Interfaces;
using MediatR;

namespace Application.Handlers.Client
{
    public static class UpdateClientCommand
    {
        public record Request(Guid ClientId, ClientRequest ClientRequest) : IRequest<Unit>;
      
        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IClientService _clientService;
            private readonly IEmailService _emailService;
            private readonly IDocumentService _documentService;

            public Handler(IClientService clientService,
                           IEmailService emailService,
                           IDocumentService documentService)
            {
                _clientService = clientService;
                _emailService = emailService;
                _documentService = documentService;
            }

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                var client = await _clientService.GetClientById(request.ClientId);
                var oldClient = client;

                client.Update(request.ClientRequest.FirstName, 
                              request.ClientRequest.LastName, 
                              request.ClientRequest.Email,
                              request.ClientRequest.PhoneNumber);

                await _clientService.UpdateClient(oldClient, client, cancellationToken);

                return Unit.Value;
            }
        }
    }
}
