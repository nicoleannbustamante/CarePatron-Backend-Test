
using Application.Handlers.Client;
using Application.Handlers.Client.GetClients;
using Application.Handlers.Client.SearchClients;
using Application.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : Controller
    {
        private readonly IMediator _mediator;

        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid Input")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(string))]
        [SwaggerOperation(
            Summary = "Creates new client",
            Description = "Creates new client, sends email and syncs document. Returns newly created client Id",
            OperationId = "Create New Client")]
        public async Task<IActionResult> CreateClient([FromBody] ClientRequest client)
        {
            var result = await _mediator.Send(new CreateClientCommand.Request(client));
            return StatusCode(StatusCodes.Status200OK, result);     
        }

        [HttpPut("{clientId:guid}")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid Input")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(Unit))]
        [SwaggerOperation(
            Summary = "Updates new client",
            Description = "Updates the client information based on the given client Id, sends email and syncs document.",
            OperationId = "Update Existing Client")]
        public async Task<IActionResult> UpdateClient([FromRoute] Guid clientId, [FromBody] ClientRequest clientUpdate)
        {
            var result = await _mediator.Send(new UpdateClientCommand.Request(clientId, clientUpdate));
            return StatusCode(StatusCodes.Status200OK, result); 
        }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid Input")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(GetClientsQuery.Response))]
        [SwaggerOperation(
            Summary = "Get Clients",
            Description = "Get all clients",
            OperationId = "Get All Client")]
        public async Task<GetClientsQuery.Response> GetAllClients()
        => await _mediator.Send(new GetClientsQuery.Request());

        [HttpGet("{searchValue}")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid Input")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(SearchClientsQuery.SearchClientResponse))]
        [SwaggerOperation(
            Summary = "Search client",
            Description = "Filter the list of clients by their first or last name ",
            OperationId = "Search Client")]
        public async Task<SearchClientsQuery.SearchClientResponse> SearchClient([FromRoute] string searchValue) 
        => await _mediator.Send(new SearchClientsQuery.Request(searchValue));

    }
}
