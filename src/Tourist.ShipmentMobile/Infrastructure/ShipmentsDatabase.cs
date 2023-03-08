using System;
using SQLite;
using Tourist.Domain;

namespace Tourist.ShipmentMobile.Infrastructure;
public class ShipmentsDatabase
{
    SQLiteAsyncConnection Database;

    public ShipmentsDatabase()
    {
    }

    public async Task Init()
    {
        if (Database is not null)
            return;

        Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        //var customerTableInfo = await Database.GetTableInfoAsync("Customer");
        //if(!customerTableInfo.Any())
        //{
        var result = await Database.CreateTablesAsync<Customer, ShipmentLineItem, Item>();
        //}
        //var shipmentTableInfo = await Database.GetTableInfoAsync("ShipmentLineItem");
        //if (!shipmentTableInfo.Any())
        //{
        //    var result = await Database.CreateTableAsync<ShipmentLineItem>();
        //}
        //SeedData();
    }

    public async Task SeedData()
    {
        var customers = await Database.Table<Customer>().ToListAsync();
        if (!customers.Any())
        {
            Customer newCustomer = new Domain.Customer()
            {
                Id = 200007,
                Name = "Инекс Олгица",
                Address = "/"
            };
            await SaveCustomerAsync(newCustomer);
            Item item1 = new Item()
            {
                Id = 1,
                Name = "Крпи"
            };
            Item item2 = new Item()
            {
                Id = 2,
                Name = "Постелнини"
            };
            await SaveItemAsync(item1);
            await SaveItemAsync(item2);

            var shipmentLineItems = await Database.Table<ShipmentLineItem>().ToListAsync();
            if (!shipmentLineItems.Any())
            {
                ShipmentLineItem shipmentItem = new Domain.ShipmentLineItem()
                {
                    Barcode = "0101010101332",
                    CustomerId = newCustomer.Id,
                    ItemId = 1,
                    ShipmentNo = "1",
                    Weight = (decimal)3.2
                };
                await SaveShipmentLineItemAsync(shipmentItem);
            }
        }
    }

    public async Task<List<Customer>> GetCustomersAsync()
    {
        await Init();
        await SeedData();
        return await Database.Table<Customer>().ToListAsync();
    }

    public async Task<List<Customer>> GetDirtyCutomers()
    {
        return await Database.Table<Customer>().Where(i => i.IsDirty).ToListAsync();
    }

    public async Task<List<Item>> GetItemsAsync()
    {
        await Init();
        return await Database.Table<Item>().ToListAsync();
    }

    public async Task<List<Item>> GetDirtyItems()
    {
        return await Database.Table<Item>().Where(i => i.IsDirty).ToListAsync();
    }

    public async Task<List<ShipmentLineItem>> GetActiveShipmentAsync(int customerId)
    {
        return await Database.Table<ShipmentLineItem>().Where(s => s.CustomerId == customerId && s.DateShipped == null).ToListAsync();
    }

    public async Task<List<ShipmentLineItem>> GetDirtyShipmentAsync()
    {
        return await Database.Table<ShipmentLineItem>().Where(s => s.IsDirty && s.DateShipped != null).ToListAsync();
    }

    public async Task<List<ShipmentDeliveredModel>> GetDeliveredShipmentsAsync()
    {
        await Init();
        return await Database.QueryAsync<ShipmentDeliveredModel>(@"
            SELECT  [ShipmentLineItem].[ShipmentNo],
                    [ShipmentLineItem].[DateShipped],
                    [Customer].[Name] as CustomerName
            FROM [ShipmentLineItem]
            INNER JOIN [Customer]
            ON [ShipmentLineItem].[CustomerId] = [Customer].[Id]
            WHERE [DateShipped] is not null ORDER BY [DateShipped] desc");
    }

    public async Task<ShipmentLineItem> GetLatestShipedItemAsync(int customerId)
    {
        return await Database.Table<ShipmentLineItem>()
                        .Where(s => s.CustomerId == customerId && s.DateShipped != null)
                        .OrderByDescending(s => s.DateShipped).FirstOrDefaultAsync();
    }

    public async Task<Customer> GetCustomerAsync(int id)
    {
        return await Database.Table<Customer>().Where(i => i.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Item> GetItemAsync(int id)
    {
        return await Database.Table<Item>().Where(i => i.Id == id).FirstOrDefaultAsync();
    }

    public async Task<ShipmentLineItem> GetShipmentLineItemAsync(int id)
    {
        return await Database.Table<ShipmentLineItem>().Where(i => i.Id == id).FirstOrDefaultAsync();
    }

    public async Task<int> SaveCustomerAsync(Customer item)
    {
        return await Insert(item);
    }

    public async Task<int> UpdateCustomerAsync(Customer item)
    {
        return await Update(item);
    }

    public async Task<int> SaveItemAsync(Item item)
    {
        return await Insert(item);
    }

    public async Task<int> UpdateItemAsync(Item item)
    {
        return await Update(item);
    }

    public async Task<int> SaveShipmentLineItemAsync(ShipmentLineItem item)
    {
        return await Insert(item);
    }

    public async Task<int> MarkAsShippedAsync(int customerId)
    {
        return await Database.ExecuteAsync("UPDATE [ShipmentLineItem] SET [DateShipped] = ?, IsDirty = 1 WHERE [DateShipped] is null and [CustomerId] = ?", DateTime.Now, customerId);
    }

    public async Task<int> DeleteCustomerAsync(Customer item)
    {
        item.IsDeleted = true;
        item.IsDirty = true;
        return await Database.DeleteAsync(item);
    }

    public async Task<int> DeleteShipmentLineItemAsync(ShipmentLineItem item)
    {
        // TO DO: how to sync hard delete items? immediate server call?
        return await Database.DeleteAsync(item);
    }

    async Task<int> Insert(ISyncEntity entity)
    {
        entity.IsDirty = true;
        return await Database.InsertAsync(entity);
    }

    async Task<int> Update(ISyncEntity entity)
    {
        entity.IsDirty = true;
        return await Database.UpdateAsync(entity);
    }
}
