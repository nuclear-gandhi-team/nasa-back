namespace Nasa.BLL.Exceptions;

public class EmailAlreadyExistException : Exception
{
    public EmailAlreadyExistException() : base("Email is already in use")
    {
    }
}