namespace Shopping.Web.Pages
{
    public class CheckoutModel(IBasketService basketService, ILogger<CheckoutModel> logger) : PageModel
    {
        [BindProperty]
        public BasketCheckoutModel Order { get; set; } = new BasketCheckoutModel();

        public ShoppingCartModel Cart { get; set; } = new ShoppingCartModel();

        public async Task<IActionResult> OnGetAsync()
        {
            Cart = await basketService.LoadUserBasket();
            return Page();
        }

        public async Task<IActionResult> OnPostCheckOutAsync()
        {
            logger.LogInformation("Checkout button clicked");
            Order.PaymentMethod = "1";

            Cart = await basketService.LoadUserBasket();

            Order.CustomerId = new Guid("58c49479-ec65-4de2-86e7-033c546291aa");
            Order.Username = Cart.UserName;
            Order.TotalPrice = Cart.TotalPrice;

            await basketService.CheckoutBasket(new CheckoutBasketRequest(Order));
            return RedirectToPage("Confirmation", "OrderSubmitted");
        }
    }
}
