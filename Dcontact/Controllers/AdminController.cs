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
                    List<Bean.User> user = d.DB_getUserforAdmin();
                    ViewBag.user = user;
                    List<Bean.User> user_block = d.DB_getBlockforAdmin();
                    ViewBag.user_block = user_block;
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Home", "Home");
            }
        }

        public ActionResult Delete_Report()
        {
            Util.DAO d = new Util.DAO();
            string id_row = Request.Form["id_row"];
            d.DB_DeleteReport(id_row);
            return RedirectToAction("admin", "Admin");
        }

        public ActionResult Accept_Report()
        {
            Util.DAO d = new Util.DAO();
            string id_row = Request.Form["id_row"];
            d.DB_AcceptReport(id_row);
            return RedirectToAction("admin", "Admin");
        }

        public ActionResult Block_User()
        {
            Util.DAO d = new Util.DAO();
            string id_user = Request.Form["id_user"];
            d.DB_BlockUser(id_user);
            return RedirectToAction("admin", "Admin");
        }

        public ActionResult Unblock_User()
        {
            Util.DAO d = new Util.DAO();
            string id_user = Request.Form["id_user"];
            d.DB_UnblockUser(id_user);
            return RedirectToAction("admin", "Admin");
        }
    }
}