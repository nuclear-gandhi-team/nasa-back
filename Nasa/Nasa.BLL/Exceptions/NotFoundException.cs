namespace Nasa.BLL.Exceptions;

public class NotFoundException: Exception
{
    public NotFoundException(string type): base($"Entity {type} was not found")
    {
    }
    
    public NotFoundException(string type, int id): base($"Entity {type} with id:{id} was not found")
    {
    }
}