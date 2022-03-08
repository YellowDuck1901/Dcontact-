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
            ViewBag.dcontact = dcontact;
            return View();
        }

        public ActionResult oder_dcard()
        {
            return View();
        }

        public ActionResult oder_dcardForm()
        {
            string mess = "message";
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
                Util.DAO d = new Util.DAO();
                float price = int.Parse(amount) * 6;
                d.DB_AddOrder(user.id, address, phone, amount, credit, cvv, exp, price.ToString(), data);
                mess = "success";
            }
            catch (Exception e)
            {
                mess = e.Message;
            }
            return Content(mess);
        }
    }
}