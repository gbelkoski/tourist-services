using Tourist.Domain;
using Tourist.Infrastructure;

namespace Tourist.Application.Queries;

public class ItemsQuery : IQuery
{
}

public class ItemsQueryHandler : IQueryHandler<ItemsQuery, IEnumerable<Item>>
{
    readonly IGenericRepository<Item> _itemsRepsository;
    public ItemsQueryHandler(IGenericRepository<Item> itemsRepository)
    {
        _itemsRepsository = itemsRepository;
    }

    public async Task<IEnumerable<Item>> HandleAsync(ItemsQuery query)
    {
        return await _itemsRepsository.SelectAll();
    }
}