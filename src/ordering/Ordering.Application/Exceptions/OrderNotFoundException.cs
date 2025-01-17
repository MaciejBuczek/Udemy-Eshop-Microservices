using Common.Exceptions;

namespace Ordering.Application.Exceptions
{
    public class OrderNotFoundException(string message) : NotFoundExcpetion(message)
    {
    }
}
