using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dcontact.Controllers
{
    public class LinkContactController : Controller
    {
        // GET: LinkContact
        public ActionResult Index()
        {
            return View();
        }
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
                Bean.Dcontact dcontact = d.DB_GetDcontact(id);
                ViewBag.dcontact = dcontact;
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
            return View();

        }

    }
}