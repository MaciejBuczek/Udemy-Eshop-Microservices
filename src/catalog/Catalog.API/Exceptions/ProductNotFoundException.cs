using Common.Exceptions;

namespace Catalog.API.Exceptions
{
    public class ProductNotFoundException(Guid id) : NotFoundExcpetion("Product", id)
    {
    }
}
