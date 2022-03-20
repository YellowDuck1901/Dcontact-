using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dcontact.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult admin()
        {
            Bean.User admin = (Bean.User)Session["user"];
            if (!admin.isAdmin) 
            {
                return RedirectToAction("dashboard", "DcontactAndDcrad");
            }
            else
            {
                return View();
            }
        }
    }
}