namespace Basket.API.Exceptions
{
    public class ShoppingCartNotFoundException(string userName) : NotFoundExcpetion("Shopping Cart", userName)
    {
    }
}
