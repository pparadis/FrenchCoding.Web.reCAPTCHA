using System.Web;
using Recaptcha.Web;
using Recaptcha.Web.Mvc;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Frenchcoding.Web.Recaptcha.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Form()
        {
            RecaptchaVerificationHelper recaptchaHelper = this.GetRecaptchaVerificationHelper();

            if (String.IsNullOrEmpty(recaptchaHelper.Response))
            {
                ModelState.AddModelError("", "Le captcha ne peut être vide.");
                return PartialView();
            }

            RecaptchaVerificationResult recaptchaResult = await recaptchaHelper.VerifyRecaptchaResponseTaskAsync();

            if (recaptchaResult != RecaptchaVerificationResult.Success)
            {
                ModelState.AddModelError("", "Réponse captcha invalide.");
                return PartialView();
            }

            if (ModelState.IsValid)
            {
                return PartialView("_FormResult");
            }

            return PartialView();
        }
    }
}