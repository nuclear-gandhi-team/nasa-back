using Nasa.DAL.Entities.Common;

namespace Nasa.DAL.Entities;

public class Settlement: BaseEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public string Сoordinates { get; set; } = string.Empty;
}