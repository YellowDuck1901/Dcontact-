using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dcontact.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Comfirm()
        {
            return View();
        }

        public ActionResult CreateNewPassword()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult RecoverPassword()
        {
            return View();
        }

        public ActionResult Sigup()
        {
            return View();
        }


        public ActionResult SigupForm(Class1 model)
        {
            String mess = "";
            if (ModelState.IsValid)
            {
                mess = model.username;

            }
            else
            {
                mess = "failed";
            }

            return RedirectToAction("Index", "Test", model);

        }

    }
}