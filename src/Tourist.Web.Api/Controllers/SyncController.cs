using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tourist.Application.Commands;
using Tourist.Application.Queries;

namespace Tourist.Web.Api.Controllers;
[ApiController]
[Authorize]
[Route("[controller]")]
public class SyncController : ControllerBase
{
    readonly ICommandDispatcher _commandDispatcher;
    readonly IQueryDispatcher _queryDispatcher;
    public SyncController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpPost(Name = "items")]
    [Route("items")]
    public async Task SyncItems(SyncItemsCommand command)
    {
        await _commandDispatcher.SendAsync(command);
    }

    [HttpPost(Name = "customers")]
    [Route("customers")]
    public async Task SyncCustomers(SyncCustomersCommand command)
    {
        await _commandDispatcher.SendAsync(command);
    }

    [HttpPost(Name = "shipments")]
    [Route("shipments")]
    public async Task SyncShipments(SyncShipmentsCommand command)
    {
        await _commandDispatcher.SendAsync(command);
    }
}
