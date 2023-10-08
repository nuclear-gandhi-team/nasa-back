namespace Nasa.BLL.Exceptions;

public class WrongEmailOrPasswordException: Exception
{
    public WrongEmailOrPasswordException() : base("Wrong email or password")
    {
    }
}