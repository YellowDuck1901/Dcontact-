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
            Util.DAO d = new Util.DAO();
            Bean.User admin = (Bean.User)Session["user"];
            if(!(admin == null))
            {
                if (!admin.isAdmin) 
                {
                    return RedirectToAction("dashboard", "DcontactAndDcrad");
                }
                else
                {
                    List<Bean.ReportLink> reportList = d.DB_getReportforAdmin();
                    ViewBag.reportList = reportList;
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Home", "Home");
            }
        }
    }
}