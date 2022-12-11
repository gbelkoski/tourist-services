using SQLite;

namespace Tourist.Domain;
public class Customer
{
    [PrimaryKey]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
}