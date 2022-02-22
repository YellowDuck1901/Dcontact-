using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Util;

namespace Dcontact.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Comfirm()
        {
            return View();
        }

        public ActionResult CreateNewPassword()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult RecoverPassword()
        {
            return View();
        }

        public ActionResult Sigup()
        {
            return View();
        }

        public ActionResult SiginForm()
        {
            String mess = "";
            string username = Request.QueryString["username"];
            string password = Request.QueryString["password"];
            //mess = username + "@" + password;
            try
            {
                Util.DAO d = new Util.DAO();
                if (d.DB_Login(username, password))
                {
                    mess = "login success!";
                    mess = "login success!";
                }
                else
                    mess = "login not success!";
            }
            catch (Exception ex)
            {
                mess = ex.Message;
            }

            //if (ModelState.IsValid)
            //{

            //}
            //else
            //{
            //}

            return Content(mess);

        }

    }
}