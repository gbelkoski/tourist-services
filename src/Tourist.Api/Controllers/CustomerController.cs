using Microsoft.AspNetCore.Mvc;
using Tourist.Application.Commands;

namespace Tourist.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    //readonly ILogger<CustomerController> _logger;
    readonly ICommandDispatcher _commandDispatcher;
    public CustomerController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost(Name = "Create")]
    public async Task Create(CreateCustomerCommand @command)
    {
        await _commandDispatcher.SendAsync(@command);
    }
}
