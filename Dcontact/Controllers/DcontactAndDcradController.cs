using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dcontact.Controllers
{
    public class DcontactAndDcradController : Controller
    {
        // GET: DcontactAndDcrad
        public ActionResult dashboard()
        {
            return View();
        }
        public ActionResult createDCard()
        {
            return View();
        }

        public ActionResult editDContact()
        {
            return View();
        }

        public ActionResult oder_dcard()
        {
            return View();
        }
    }
}