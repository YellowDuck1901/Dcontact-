using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dcontact.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Home()
        {
            Util.DAO dAO = new Util.DAO();
            dAO.DB_addAccess();
            return View();
        }
    }
}