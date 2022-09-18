using Microsoft.AspNetCore.Mvc;
using Tourist.Application.Commands;
using Tourist.Application.Queries;
using Tourist.Domain;

namespace Tourist.Api.Controllers;

[ApiController]
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

    [HttpPost(Name = "Create")]
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
