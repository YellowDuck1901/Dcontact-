
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Util;
namespace Dcontact.Controllers { 
public class LinkContactController : Controller
    {
    

        //}
        //public ActionResult LinkContact(string username)
        //{
        //    String mess = "";
        //    try
        //    {
        //        Util.DAO d = new Util.DAO();
        //        Bean.Dcontact dcontact = d.DB_GetDcontact(Util.MD5.CreateMD5(username));   //khoi tao object user voi data từ db
        //        ViewBag.link = dcontact;
        //        //return RedirectToAction("LinkContact", "LinkContact");
        //    }
        //    catch (Exception ex)
        //    {
        //        mess = ex.Message;
        //    }
        //    //return RedirectToAction("LinkContact", "LinkContact", new { msg = mess });
        //    return View();
        //}

        public ActionResult LinkContact(string username)
        {
            try
            {
                ViewBag.name = username;
                Util.DAO d = new Util.DAO();
                string id = Util.MD5.CreateMD5(username);
                Session["linkdcontact"] = id;
                Bean.Dcontact dcontact = d.DB_GetDcontact(id);
                ViewBag.dcontact = dcontact;
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