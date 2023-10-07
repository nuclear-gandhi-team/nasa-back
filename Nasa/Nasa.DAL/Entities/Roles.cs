using Nasa.DAL.Entities.Common;

namespace Nasa.DAL.Entities;

public class Role: BaseEntity<int>
{
    public string Name { get; set; } = string.Empty;
}