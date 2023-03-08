using System;
namespace Tourist.Domain;
public interface ISoftDelete
{
    bool IsDeleted { get; set; }
}
