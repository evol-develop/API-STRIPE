using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using API_STRIPE.Models;

namespace API_STRIPE.Controllers
{
    [Route("api/stripe")]
    [ApiController]
    public class Stripe : ControllerBase
    {
        [HttpPost]
        [Route("checkout")]
        public ActionResult checkout(List<Articulos> articulo)
        {



            string StripeSecretKey = articulo[0].UserKey;

            StripeConfiguration.ApiKey = StripeSecretKey;

            var lineItems = articulo.Select(product => new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "mxn",
                    UnitAmount = product.Costo * 100, // Precio en centavos
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        
                        Name = product.Nombre
                    }
                },
                Quantity = product.Quantity
            }).ToList();

            var options = new SessionCreateOptions
            {
                CustomerEmail = articulo[0].Email,
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = lineItems,
                Mode = "payment",
                Locale = "es",
                SuccessUrl = "http://localhost:3000/success",
                CancelUrl = "http://localhost:3000/cancel"
            };

            var service = new SessionService();
            var session = service.Create(options);

            return Ok(new { Url = session.Url });
        }
    }


}