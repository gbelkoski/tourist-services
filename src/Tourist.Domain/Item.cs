using SQLite;

namespace Tourist.Domain;
public class Item
{
    [PrimaryKey]
    public int Id { get; set; }
    public string Name { get; set; }
}