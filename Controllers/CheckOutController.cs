using E_Commerce.Models;
using E_Commerce.Repository;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Collections.Generic;
using System.Security.Claims;
namespace E_Commerce.Controllers
{
    public class CheckOutController : Controller
    {
        IProductRepository productRepository;
        private readonly IShoppingService shoppingService;

        public CheckOutController(IProductRepository productRepository,IShoppingService shoppingService)
        {
            this.productRepository = productRepository;
            this.shoppingService = shoppingService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CheckOutAll()
        {
            var domain = "http://localhost:21741/";
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(); 
            }

            var cart = shoppingService.GetByUserId(userId); 

            if (cart == null)
            {
                return NotFound("Cart not found.");
            }

            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + "Checkout/OrderConfirmation",
                CancelUrl = domain + "Checkout/Login",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                
            };

            foreach (var cartItem in cart.CartItems)
            {
                var product = productRepository.GetById(cartItem.ProductId);

                if (product == null)
                {
                    // Handle the case when product is not found
                    return NotFound($"Product with ID {cartItem.ProductId} not found.");
                }

                if (product.Price <= 0)
                {
                    // Handle invalid price
                    return BadRequest($"Invalid price for product {product.Name}.");
                }

                if (string.IsNullOrEmpty(product.Name))
                {
                    // Handle invalid product name
                    return BadRequest("Product name is missing.");
                }

                var sessionListProduct = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(product.Price * 100),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = product.Name,
                        }
                    },
                    Quantity = 1,
                };

                options.LineItems.Add(sessionListProduct);
            }

            var service = new SessionService();
            Session session = service.Create(options);

            TempData["Session"] = session.Id;
            Response.Headers.Add("Location", session.Url);

            return new StatusCodeResult(303);
        }

        public IActionResult CheckOut(int productId)
        {
            var domain = "http://localhost:21741/";
            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + $"Checkout/OrderConfirmation",
                CancelUrl = domain + $"Checkout/Login",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
               // CustomerEmail = "ahmedgamal232003@gmail.com"
            };
            var product = productRepository.GetById(productId);
            
                var sessionListProduct = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions {

                        UnitAmount = (long)(product.Price*100),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = product.Name.ToString(),
                        } 
                    },
                    Quantity=1,
                };
                options.LineItems.Add(sessionListProduct);
                var service =new SessionService();
                Session session = service.Create(options);
                TempData["Session"] = session.Id;
                Response.Headers.Add("Location", session.Url);
                return new StatusCodeResult(303);

            

            return View();
        }


        

        public IActionResult OrderConfirmation()
        {
            var service =new SessionService();
            Session session = service.Get(TempData["Session"].ToString());
            if (session.PaymentStatus == "Paid")
            {
                return View("Success");
            }
            return View("Login");
        }

        public IActionResult Success()
        {
            return View();
        }
        public IActionResult Login() 
        {
            return View();
        }
    }
}
