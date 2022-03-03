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

            Util.DAO d = new Util.DAO();
            var user = (Bean.User)Session["user"];
            Bean.Dcontact dcontact = d.DB_GetDcontact(user.id);          
            ViewBag.dcontact = dcontact;
           return View();
        }
        public ActionResult createDCard()
        {
            return View();
        }

        public ActionResult editDContact()
        {
            Util.DAO d = new Util.DAO();
            var user = (Bean.User)Session["user"];
            Bean.Dcontact dcontact =  d.DB_GetDcontact(user.id);
            ViewBag.numerView = dcontact.numerView;
            ViewBag.avt = dcontact.avt;
            ViewBag.background = dcontact.background;
            ViewBag.rows = dcontact.rows;
            return View();
        }

        public ActionResult oder_dcard()
        {
            return View();
        }
    }
}