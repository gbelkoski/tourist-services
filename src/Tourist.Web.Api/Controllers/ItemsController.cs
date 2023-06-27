using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tourist.Application.Commands;
using Tourist.Application.Queries;
using Tourist.Domain;

namespace Tourist.Web.Api.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class ItemsController : ControllerBase
{
    readonly IQueryDispatcher _queryDispatcher;
    readonly ICommandDispatcher _commandDispatcher;
    public ItemsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    public async Task Create(CreateItemCommand @command)
    {
        await _commandDispatcher.SendAsync(@command);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] ItemsQuery query)
    {
        var result = await _queryDispatcher.QueryAsync<IEnumerable<Item>, ItemsQuery>(query);
        return Ok(result);
    }
}
