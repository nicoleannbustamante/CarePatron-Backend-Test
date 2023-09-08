using Application.Models.Responses;
using Application.Services.Interfaces;
using MediatR;

namespace Application.Handlers.Client.SearchClients
{
    public static partial class SearchClientsQuery
    {
        public record Request(string SearchValue) : IRequest<SearchClientResponse>;
        public record SearchClientResponse(IEnumerable<ClientResponse> Clients);

        public class Handler : IRequestHandler<Request, SearchClientResponse>
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

            //    var query = @"SELECT * FROM Client WHERE FirstName = @FirstName OR LastName = @LastName";

            //    var clients = await connection.QueryAsync<clientDomain.Client>(query, new {FirstName = request.SearchValue, LastName = request.SearchValue);

            //    return new Response(clients);
            //}

            #endregion

            private readonly IClientService _clientService;
            public Handler(IClientService clientService)
            {
                _clientService = clientService;
            }

            public async Task<SearchClientResponse> Handle(Request request, CancellationToken cancellation)
            {
                var clients = await _clientService.SearchClients(request.SearchValue);

                return new SearchClientResponse(clients);
            }

        }
    }
}
