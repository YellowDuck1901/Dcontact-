using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using FSharp.Data.Runtime;

namespace Dcontact.Controllers
{
    [HandleError]
    public class DcontactAndDcradController : Controller
    {
        // GET: DcontactAndDcrad
        public ActionResult dashboard()
        {
            Util.DAO d = new Util.DAO();
            var user = (Bean.User)Session["user"];
            if (user == null || user.isAdmin)
            {
                return RedirectToAction("Home", "Home");
            }
            else
            {
                Bean.Dcontact dcontact = d.DB_GetDcontact(user.id);
                ViewBag.dcontact = dcontact;
                return View();
            }
        }
        public ActionResult createDCard()
        {
            var user = (Bean.User)Session["user"];
            if (user == null)
            {
                return RedirectToAction("Home", "Home");
            }
            else
            {
                return View();
            }
        }

        public ActionResult upURL()
        {

            string image_merge = Request.Form["image_merge"];
            ViewBag.image_merge = image_merge;
            return Content(image_merge);
        }

        public ActionResult editDContact()
        {
            Util.DAO d = new Util.DAO();
            var user = (Bean.User)Session["user"];
            if (user == null || user.isAdmin)
            {
                return RedirectToAction("Home", "Home");
            }
            else
            {
                Bean.Dcontact dcontact = d.DB_GetDcontact(user.id);
                ViewBag.dcontact = dcontact;
                ViewBag.template = d.DB_loadTemplate(user.id);
                return View();
            }
        }

        public ActionResult oder_dcard(string image_merge_url)
        {
            ViewBag.image_merge_url = image_merge_url;
            var user = (Bean.User)Session["user"];
            if (user == null || user.isAdmin)
            {
                return RedirectToAction("Home", "Home");
            }
            else
            {
                return View();
            }
        }

        public ActionResult oder_dcardForm()
        {
            string mess = "message: ";
            try
            {
                Bean.User user = (Bean.User)Session["user"];
                string data = Request.Form["cardBackGround"];
                string address = Request.Form["address"];
                string phone = Request.Form["phone"];
                string amount = Request.Form["amount"];
                string credit = Request.Form["cardNumber"];
                string cvv = Request.Form["cvv"];
                string exp = Request.Form["exp"];
                mess = exp;
                Util.DAO d = new Util.DAO();
                float price = int.Parse(amount) * 6;
                d.DB_AddOrder(user.id, address, phone, amount, credit, cvv, exp, price.ToString(), data);
                mess = "success";
            }
            catch (Exception e)
            {
                mess += e.Message;
            }
            return Content(mess);
        }
        public ActionResult addRow()
        {
            string mess = "message";
            try
            {
                Bean.User user = (Bean.User)Session["user"];
                Bean.Row r = new Bean.Row(Util.UUID.getUUID(), user.username);
                Util.DAO d = new Util.DAO();
                d.DB_AddRow(r.ID, user.id, r.text, r.font, r.color, r.link, r.bullet, r.code, "9999-1-1", "0");
                string row = $"<li id ='{r.ID}'> <span class='report'> <abbr title = 'Click here to delete this link' > <i class='fa fa-trash-o'> </i> </abbr> </span> <div class='button'role='button' id='{@Util.UUID.getUUID()}' style='background-color: {r.color};height: 26.875px' url='{r.link}' code = '{r.code}'> <i class='{r.bullet}'></i> <div class='card--item__text'> <label style = 'font-family: '{r.font}';'>{r.text}</label> </div> </div> </li>";
                return Content(row);

            }
            catch (Exception e)
            {
                mess = e.Message;
            }
            return Content("");
        }
        public ActionResult deleteRow()
        {
            string mess = "message";
            try
            {
                Bean.User user = (Bean.User)Session["user"];
                Util.DAO d = new Util.DAO();
                string id = Request.Form["id"];
                d.DB_DelRow(id, user.id);
                Bean.Dcontact dcontact = d.DB_GetDcontact(user.id);
                ViewBag.dcontact = dcontact;
                //xoa row trong db
                return Content("");
            }
            catch (Exception e)
            {
                mess = e.Message;
            }
            return Content("");
        }

        public ActionResult updateRow()
        {
            string mess = "message";
            try
            {
                Bean.User user = (Bean.User)Session["user"];
                string idRow = Request.Form["id_row"];
                string text = Request.Form["text_row"];
                string bullet = Request.Form["bullet_row"];
                string color = Request.Form["color_row"];
                string font = Request.Form["font_row"];
                string bdday = Request.Form["bdate_row"];
                string link = Request.Form["link_row"];
                string code = Request.Form["code_row"];

                //link
                Console.WriteLine(font);
                Util.DAO d = new Util.DAO();
                d.DB_UpdateRow(idRow, user.id, text, font, color, link, bullet, code, bdday);
                return Content("");
            }
            catch (Exception e)
            {
                mess = e.Message;
            }
            return Content(mess);
        }

        public ActionResult updateImage()
        {
            Bean.User user = (Bean.User)Session["user"];
            string path = Request.Form["path"];
            string piece = Request.Form["piece"];
            Util.DAO d = new Util.DAO();
            switch (piece)
            {
                case "avatar":
                    d.DB_updateAvt(user.id, path);
                    break;
                case "updateTemplate":
                    d.DB_updateTemplate(user.id, path);
                    break;
                case "addAndUpdateTemplate":
                    d.DB_addTemplate(Util.UUID.getUUID(), user.id, path);
                    d.DB_updateTemplate(user.id, path);
                    break;
            }
            return new HttpStatusCodeResult(200);
        }
        public void DeleteFileFromFolder()
        {
            string StrFilename = Request.Form["path"];

            string strPhysicalFolder = Server.MapPath("..\\");

            string strFileFullPath = strPhysicalFolder + StrFilename;

            if (System.IO.File.Exists(strFileFullPath))
            {
                System.IO.File.Delete(strFileFullPath);
            }
        }
    }
}