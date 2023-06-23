using SQLite;

namespace Tourist.Domain;
public class Item : ISyncEntity, ISoftDelete
{
    [PrimaryKey]
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public bool IsDirty { get; set; }
    public bool IsDeleted { get; set; }
}