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
                    d.DB_addView(id);
                }
            }
            catch (Exception ex)
            {
                TempData["ex"] = ex.Message;
                return RedirectToAction("Error", "Shared");
            }
            return View();
        }

        public ActionResult GetLink()
        {
            DAO d = new Util.DAO();
            string id_row = Request.Form["id"];
            string url = d.DB_GetLink(id_row);
            d.DB_addClick(id_row);
            return Content(url, "text/html");
        }
        public ActionResult GetLink(string id_row)
        {
            DAO d = new Util.DAO();
            string url = d.DB_GetLink(id_row);
            d.DB_addClick(id_row);
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

        public ActionResult sendCode()
        {
            try
            {
                Util.DAO d = new Util.DAO();
                var vertifyCode = RandomCode.Random_6D();
                string email = Request.Form["email"];
                Mail.send(email, "Code to Verify Email", vertifyCode);
                Session.Add("email", email);            //session luu tru email
                Session.Add("" +email, vertifyCode);   //key la email con du lieu tren session cua email la vertifycode
                Session.Add("VerifyCodeExpiry", true);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
            return Content("");
        }

        public ActionResult Vertification()
        {
            try
            {
                string vertifyCode = Request.Form["code"];
                if (vertifyCode.Equals((string)Session[(string)Session["email"]]))
                {
                    Session["VerifyCodeExpiry"] = false;  //remove value cho phep doi password
                    Session.Remove((string)Session[(string)Session["email"]]);  //xoa cap value email
                    Session.Remove((string)Session["email"]);
                    return Content("equal");
                }
                else
                {
                    return Content("notEqual");
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
            return Content("");
        }
    }
}
