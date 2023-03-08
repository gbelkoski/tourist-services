using System;
using Tourist.Domain;

namespace Tourist.Application.Commands;
public class SyncItemsCommand : ICommand
{
    public List<Item> Items { get; set; }
}
