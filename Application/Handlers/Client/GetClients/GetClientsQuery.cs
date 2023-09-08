using Application.Models.Responses;
using Application.Services.Interfaces;
using MediatR;

namespace Application.Handlers.Client.GetClients
{
    public static partial class GetClientsQuery
    {
        public record Request : IRequest<Response>;
        public record Response(IEnumerable<ClientResponse> Clients);

        public class Handler : IRequestHandler<Request, Response>
        {

            #region Solution if there is a DB (using Dapper)

            //Note: ISqlConnectionFactory this is an interface to be created for creating a connection based on the connection string

            //private readonly ISqlConnectionFactory _sqlConnectionFactory;
            //public Handler(ISqlConnectionFactory sqlConnectionFactory)
            //{
            //    _sqlConnectionFactory = sqlConnectionFactory;
            //}

            //public async Task<Response> Handle(Request request, CancellationToken cancellation)
            //{
            //    using var connection = await _sqlConnectionFactory.GetOpenConnection();

            //    var query = @"SELECT * FROM Client";

            //    var clients = await connection.QueryAsync<clientDomain.Client>(query);

            //    return new Response(clients);
            //}

            #endregion

            private readonly IClientService _clientService;
            public Handler(IClientService clientService)
            {
                _clientService = clientService;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellation)
            {
                var clients = await _clientService.GetAllClients();
                return new Response(clients);
            }

        }
    }
}
