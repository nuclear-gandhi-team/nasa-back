using System.Net;
using Nasa.BLL.Exceptions;

namespace Nasa.API.Extensions;

public static class ExceptionExtension
{
    public static HttpStatusCode GetStatusCode(this Exception exception)
    {
        return exception switch
        {
            NotFoundException => HttpStatusCode.NotFound,
            EmailAlreadyExistException => HttpStatusCode.BadRequest,
            WrongEmailOrPasswordException => HttpStatusCode.BadRequest,
            CurrentFiresApiException => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.InternalServerError
        };
    }
}