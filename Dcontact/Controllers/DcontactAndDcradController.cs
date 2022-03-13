using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            if ((user.isAdmin) || user == null)
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
            return View();
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
                Bean.Dcontact dcontact =  d.DB_GetDcontact(user.id);
                ViewBag.dcontact = dcontact;
                return View();
            }
        }

        public ActionResult oder_dcard()
        {
            var user = (Bean.User)Session["user"];
            if ((user.isAdmin) || user == null)
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
                Bean.Row r = new Bean.Row();
                 Util.DAO d = new Util.DAO();
                d.DB_AddRow(r.ID, user.id,r.text,r.font,r.color,"link",r.bullet,"1111","1-2-1232","111");
                string row = $"<li id ='{r.ID}'> <span class='report'> <abbr title = 'Click here to delete this link' > <i class='fa fa-trash-o'> </i> </abbr> </span> <div class='button'role='button' id='{@Util.UUID.getUUID()}' style='background-color: {r.color}'> <i class='{r.bullet}'></i> <div class='card--item__text'> <label style = 'font-family: '{r.font}';'>{r.text}</label> </div> </div> </li>";
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
                //link
                Console.WriteLine(font);
                Util.DAO d = new Util.DAO();
                d.DB_UpdateRow(idRow, user.id, text, font, color, "abc.com", bullet, "1234", "2331-2-12", "1000");
                return Content("");
            }
            catch (Exception e)
            {
                mess = e.Message;
            }
            return Content("");
        }
        public ActionResult updateImage()
        {
            Bean.User user = (Bean.User)Session["user"];
            user.dcontact.avt = Request.Form["path"];
            return new HttpStatusCodeResult(200);
        }
    }
}