using System;
using SQLite;
using Tourist.Domain;
using Cleanex.ClientApp.Models;

namespace Cleanex.ClientApp.Infrastructure;
public class ShipmentsDatabase
{
    SQLiteAsyncConnection _dbConnection;

    public async Task Init()
    {
        if (_dbConnection is not null)
            return;

        _dbConnection = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        var result = await _dbConnection.CreateTablesAsync<Customer, ShipmentLineItem, Item>();
    }

    public async Task SeedData()
    {
        var customers = await _dbConnection.Table<Customer>().ToListAsync();
        if (!customers.Any())
        {
            Customer newCustomer = new Customer()
            {
                Id = 200007,
                Name = "Инекс Олгица",
                Address = "/",
                IsDirty = true
            };
            await SaveCustomerAsync(newCustomer);
            Item item1 = new Item()
            {
                Id = 1,
                Name = "Крпи",
                IsDirty = true
            };
            Item item2 = new Item()
            {
                Id = 2,
                Name = "Постелнини",
                IsDirty = true
            };
            await SaveItemAsync(item1);
            await SaveItemAsync(item2);

            var shipmentLineItems = await _dbConnection.Table<ShipmentLineItem>().ToListAsync();
            if (!shipmentLineItems.Any())
            {
                ShipmentLineItem shipmentItem = new ShipmentLineItem()
                {
                    Barcode = "0101010101332",
                    CustomerId = newCustomer.Id,
                    ItemId = 1,
                    ShipmentNo = "1",
                    Weight = (decimal)3.2,
                    IsDirty = true
                };
                await SaveShipmentLineItemAsync(shipmentItem);
            }
        }
    }

    public async Task<List<Customer>> GetCustomersAsync()
    {
        await Init();
        return await _dbConnection.Table<Customer>().ToListAsync();
    }

    public async Task<List<Customer>> GetDirtyCutomers()
    {
        await Init();
        return await _dbConnection.Table<Customer>().Where(i => i.IsDirty).ToListAsync();
    }

    public async Task<List<Item>> GetItemsAsync()
    {
        await Init();
        return await _dbConnection.Table<Item>().ToListAsync();
    }

    public async Task<List<Item>> GetDirtyItems()
    {
        await Init();
        return await _dbConnection.Table<Item>().Where(i => i.IsDirty).ToListAsync();
    }

    public async Task<List<ShipmentLineItem>> GetActiveShipmentAsync(int customerId)
    {
        return await _dbConnection.Table<ShipmentLineItem>().Where(s => s.CustomerId == customerId && s.DateShipped == null).ToListAsync();
    }

    public async Task<List<ShipmentLineItem>> GetDirtyShipmentAsync()
    {
        return await _dbConnection.Table<ShipmentLineItem>().Where(s => s.IsDirty && s.DateShipped != null).ToListAsync();
    }

    public async Task<List<DeliveredShipmentModel>> GetDeliveredShipmentsAsync()
    {
        await Init();
        return await _dbConnection.QueryAsync<DeliveredShipmentModel>(@"
            SELECT  [ShipmentLineItem].[ShipmentNo],
                    [ShipmentLineItem].[DateShipped],
                    [ShipmentLineItem].[CustomerId],
                    [Customer].[Name] as CustomerName,
                    [ShipmentLineItem].[IsDirty]
            FROM [ShipmentLineItem]
            INNER JOIN [Customer]
            ON [ShipmentLineItem].[CustomerId] = [Customer].[Id]
            WHERE [DateShipped] is not null ORDER BY [DateShipped] desc");
    }

    public async Task<ShipmentLineItem> GetLatestShipedItemAsync(int customerId)
    {
        return await _dbConnection.Table<ShipmentLineItem>()
                        .Where(s => s.CustomerId == customerId && s.DateShipped != null)
                        .OrderByDescending(s => s.DateShipped).FirstOrDefaultAsync();
    }

    public async Task<Customer> GetCustomerAsync(int id)
    {
        return await _dbConnection.Table<Customer>().Where(i => i.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Customer> GetCustomerAsync(string code)
    {
        return await _dbConnection.Table<Customer>().Where(i => i.Code == code).FirstOrDefaultAsync();
    }

    public async Task<Item> GetItemAsync(int id)
    {
        await Init();
        return await _dbConnection.Table<Item>().Where(i => i.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Item> GetItemAsync(string code)
    {
        await Init();
        return await _dbConnection.Table<Item>().Where(i => i.Code == code).FirstOrDefaultAsync();
    }

    public async Task<ShipmentLineItem> GetShipmentLineItemAsync(int id)
    {
        return await _dbConnection.Table<ShipmentLineItem>().Where(i => i.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<ShipmentLineItem>> GetShipmentLineItemsAsync(string shipmentNo, int customerId, DateTime shipmentDate)
    {
        return await _dbConnection.Table<ShipmentLineItem>()
            .Where(i => i.ShipmentNo == shipmentNo
                            && i.CustomerId == customerId
                            && i.DateShipped == shipmentDate)
            .ToListAsync();
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

    public async Task<int> DeleteItemAsync(Item item)
    {
        if (item == null) return -1;

        return await _dbConnection.DeleteAsync(item);
    }

    public async Task<int> SaveShipmentLineItemAsync(ShipmentLineItem item)
    {
        return await Insert(item);
    }

    public async Task<int> UpdateShipmentLineItemAsync(ShipmentLineItem item)
    {
        return await Update(item);
    }

    public async Task<int> MarkAsShippedAsync(int customerId)
    {
        return await _dbConnection.ExecuteAsync("UPDATE [ShipmentLineItem] SET [DateShipped] = ?, IsDirty = 1 WHERE [DateShipped] is null and [CustomerId] = ?", DateTime.Now, customerId);
    }

    public async Task<int> MarkEntityNotDirty(ISyncEntity entity)
    {
        entity.IsDirty = false;
        return await _dbConnection.UpdateAsync(entity);
    }

    public async Task<int> DeleteCustomerAsync(Customer item)
    {
        if (item == null) return -1;

        item.IsDeleted = true;
        item.IsDirty = true;
        return await _dbConnection.DeleteAsync(item);
    }

    public async Task<int> DeleteShipmentLineItemAsync(ShipmentLineItem item)
    {
        if (item == null) return -1;
        return await _dbConnection.DeleteAsync(item);
    }

    async Task<int> Insert(ISyncEntity entity)
    {
        entity.IsDirty = true;
        return await _dbConnection.InsertAsync(entity);
    }

    async Task<int> Update(ISyncEntity entity)
    {
        entity.IsDirty = true;
        return await _dbConnection.UpdateAsync(entity);
    }
}
