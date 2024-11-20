using Microsoft.AspNetCore.Mvc;
using Northwind.Core.Models.Request.Payment;
using Stripe;
using Stripe.Checkout;
using Stripe.V2;

namespace Northwind.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : Controller
    {
        
        [HttpGet]
        public ActionResult SessionStatus([FromQuery] string session_id)
        {
            var sessionService = new SessionService();
            Session session = sessionService.Get(session_id);

            return Json(new { status = session.Status, customer_email = session.CustomerDetails.Email });
        }


        [HttpPost("create-intent")]
        public ActionResult Create([FromBody] PaymentRequestModel paymentRequestModel)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = paymentRequestModel.Amount,
                Currency = paymentRequestModel.currency,
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                },
            };
            var service = new PaymentIntentService();
            PaymentIntent intent = service.Create(options);

            return Json(new { client_secret = intent.ClientSecret });
        }
}
}
