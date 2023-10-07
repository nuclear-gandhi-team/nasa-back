namespace Nasa.DAL.Entities.Common;

public abstract class BaseEntity<T> where T : struct
{
    public T Id { get; set; }
}