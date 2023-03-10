using Tourist.Domain;
using Tourist.Infrastructure;

namespace Tourist.Application.Commands;
public class SyncItemsCommand : ICommand
{
    public List<Item> Items { get; set; }
}

public class SyncItemsCommandHandler : ICommandHandler<SyncItemsCommand>
{
    readonly IGenericRepository<Item> _itemRepository;

    public SyncItemsCommandHandler(IGenericRepository<Item> itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public async Task HandleAsync(SyncItemsCommand command)
    {
        foreach(var item in command.Items)
        {
            var dbItem = await _itemRepository.SelectById(item.Id);
            if(dbItem == null)
            {
                await _itemRepository.Insert(new Item()
                {
                    Id = item.Id,
                    Name = item.Name,
                    IsDeleted = item.IsDeleted
                });
            }
            else
            {
                dbItem.Name = item.Name;
                dbItem.IsDeleted = item.IsDeleted;
                await _itemRepository.Update(dbItem);
            }
        }
    }
}
