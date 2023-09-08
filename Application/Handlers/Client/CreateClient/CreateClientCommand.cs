using Application.Models.Requests;
using Application.Services.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using clientDomain = Domain.Aggregates.Clients;

namespace Application.Handlers.Client
{
    public static class CreateClientCommand
    {
        public record Response(Guid Id);
        public record Request(ClientRequest ClientRequest) : IRequest<Response>;

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IClientService _clientService;
            public Handler(IClientService clientService)
            {
                _clientService = clientService;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var client = clientDomain.Client.CreateClient(request.ClientRequest.FirstName,
                                                        request.ClientRequest.LastName,
                                                        request.ClientRequest.Email,
                                                        request.ClientRequest.PhoneNumber);

                await _clientService.CreateClient(client, cancellationToken);

                return new Response(client.Id);             
            }
        }
    }
}
