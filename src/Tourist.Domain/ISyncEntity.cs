using System;
namespace Tourist.Domain;
public interface ISyncEntity
{
    bool IsDirty { get; set; }
}
