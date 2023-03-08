using System;
using Microsoft.AspNetCore.Mvc;
using Tourist.Application.Commands;
using Tourist.Application.Queries;
using Tourist.Domain;

namespace Tourist.Web.Api.Controllers;
[ApiController]
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
    public async Task SyncItems(SyncItemsCommand command)
    {
        await _commandDispatcher.SendAsync(command);
    }

    [HttpPost("[action]")]
    public async Task SyncCustomers(SyncCustomersCommand command)
    {
        await _commandDispatcher.SendAsync(command);
    }

    [HttpPost(Name = "shipments")]
    public async Task SyncShipments(SyncShipmentsCommand command)
    {
        await _commandDispatcher.SendAsync(command);
    }
}
