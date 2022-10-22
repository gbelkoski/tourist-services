using Microsoft.AspNetCore.Mvc;
using Tourist.Application.Commands;
using Tourist.Application.Queries;
using Tourist.Domain;

namespace Tourist.Web.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ShipmentsController : ControllerBase
{
    readonly ICommandDispatcher _commandDispatcher;
    readonly IQueryDispatcher _queryDispatcher;
    public ShipmentsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpPost(Name = "Create")]
    public async Task CreateShipmentLineItem()
    {
        // TO DO: Create new item to be shipped
        //await _commandDispatcher.SendAsync(@command);
        throw new NotImplementedException();
    }

    [HttpGet]
    public async Task<IActionResult> GetActiveShipmentDetails()
    {
        // TO DO: Return the non-shipped items for given customer
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task DeliverShipment()
    {
        // TO DO: Mark all active line items as shipped for given customer
        // Assign shipment number to all line items
        throw new NotImplementedException();
    }

    [HttpGet]
    public async Task<IActionResult> GetDeliveredShipmentDetails()
    {
        // TO DO: Return the shipped items for given shipment number/id
        throw new NotImplementedException();
    }

}
