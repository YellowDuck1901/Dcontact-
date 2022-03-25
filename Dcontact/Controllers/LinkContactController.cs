using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Util;
using System.Web.Http;

namespace Dcontact.Controllers
{
    public class LinkContactController : Controller
    {
        public ActionResult LinkContact(string username)
        {
            try
            {
                ViewBag.name = username;
                Util.DAO d = new Util.DAO();
                string id = Util.MD5.CreateMD5(username);
                Session["linkdcontact"] = id;
                if (d.DB_CheckUserBlock(id))
                {
                    return RedirectToAction("Error", "Shared");
                }
                else
                {
                    Bean.Dcontact dcontact = d.DB_GetDcontact(id);
                    ViewBag.dcontact = dcontact;
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Shared");
            }
            return View();
        }

        public ActionResult GetLink()
        {
            DAO d = new Util.DAO();
            string id_row = Request.Form["id"];
            string url = d.DB_GetLink(id_row);
            return Content(url, "text/html");
        }
        public ActionResult GetLink(string id_row)
        {
            DAO d = new Util.DAO();
            string url = d.DB_GetLink(id_row);
            return Content(url, "text/html");
        }
        public ActionResult gate()
        {
            DAO d = new Util.DAO();
            string id_row = Request.Form["id"];
            if (d.DB_gate(id_row))
            {
                return Content("", "text/html");
            }
            else
            {
                return GetLink(id_row);
            }

        }
        public ActionResult gate_code([FromBody] string id_row, [FromBody] string code)
        {
            DAO d = new Util.DAO();

            if (d.DB_gateCodeconfirm(id_row, code))
            {
                return GetLink(id_row);
            }
            else
            {
                return Content("", "text/html");
            }

        }

        public ActionResult Report()
        {
            try
            {
                Util.DAO d = new Util.DAO();
                string id_row = Request.Form["id_row"];
                string txt_des = Request.Form["txt_Des"];
                string iduser1 = Session["linkdcontact"].ToString();
                d.DB_AddReportofGuest(iduser1, id_row, txt_des);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
            return Content("acb");
        }
    }
}