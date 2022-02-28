using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dcontact.Controllers
{
    public class SharedController : Controller
    {
        // GET: Shared
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Header()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Session.RemoveAll();
            Session.Clear();
            return RedirectToAction("Home", "Home");
        }

        public ActionResult Error()
        {
            return View();
        }

      /*  [HttpPost]
        public IActionResult Update(TestViewModel model)
        {
            return RedirectToAction("Index", new { name = model.Name, desc = model.desc });
        }*/
    }
}