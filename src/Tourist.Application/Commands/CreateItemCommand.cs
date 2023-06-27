using System.Text.Json.Serialization;
using Tourist.Domain;
using Tourist.Infrastructure;

namespace Tourist.Application.Commands;

public class CreateItemCommand : ICommand
{
    [JsonPropertyName("Id")]
    public int Id { get; set; }
    [JsonPropertyName("Name")]
    public string Name { get; set; }
    [JsonPropertyName("Code")]
    public string Code { get; set; }
}

public class CreateItemCommandHandler : ICommandHandler<CreateItemCommand>
{
    readonly IGenericRepository<Item> _itemsRepository;

    public CreateItemCommandHandler(IGenericRepository<Item> itemsRepository)
    {
        _itemsRepository = itemsRepository;
    }
    
    public async Task HandleAsync(CreateItemCommand command)
    {
        await _itemsRepository.Insert(new Item()
        {
            Id = command.Id,
            Name = command.Name, 
            Code = command.Code
        });
    }
}