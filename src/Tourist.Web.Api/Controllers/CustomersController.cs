using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tourist.Application.Commands;
using Tourist.Application.Queries;
using Tourist.Domain;

namespace Tourist.Web.Api.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class CustomersController : ControllerBase
{
    readonly ICommandDispatcher _commandDispatcher;
    readonly IQueryDispatcher _queryDispatcher;
    public CustomersController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpPost]
    public async Task Create(CreateCustomerCommand @command)
    {
        await _commandDispatcher.SendAsync(@command);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] CustomersQuery query)
    {
        var result = await _queryDispatcher.QueryAsync<IEnumerable<Customer>, CustomersQuery>(query);
        return Ok(result);
    }
}
