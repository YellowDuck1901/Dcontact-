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

        public ActionResult Login(String msg)
        {
            ViewBag.msg = msg;  
            return View();
        }

         public ActionResult LoginForm()
        {
            String mess = "";
            string username = Request.QueryString["username"];  //get value tag input with name is "username"
            string password = Request.QueryString["password"];
            //mess = username + "@" + password;
            try
            {
                Util.DAO d = new Util.DAO();                    
                if (d.DB_Login(username, password))
                {
                    Bean.User user = d.DB_getUser(username);   //khoi tao object user voi data từ db
                    Session.Add("user", user);                                          // ("key","object")
                    return RedirectToAction("dashboard", "DcontactAndDcrad");
                }
              
            }
            catch (Exception ex)
            {
             mess = ex.Message; 
            }
            return RedirectToAction("Login", "Account",new { msg = mess});   
        }

      

        public ActionResult RecoverPassword()
        {
            return View();
        }

        public ActionResult Sigup(String msg)
        {
            ViewBag.msg = msg;
            return View();
        }

        public ActionResult SigupForm()
        {
            String mess = "";
            string username = Request.QueryString["username"];
            string password = Request.QueryString["password"];
            string email = Request.QueryString["email"];

            //mess = username + "@" + password;
            try
            {
                Util.DAO d = new Util.DAO();
                if (d.DB_SignUp(username, email, password))
                {
                    Bean.User user = d.DB_getUser(username);   //khoi tao object user voi data từ db
                    Session.Add("user", user);                                          // ("key","object")
                    return RedirectToAction("editDContact", "DcontactAndDcrad");
                }
                else
                {
                    Console.WriteLine("sigup falled");
                }
                    
            }
            catch (Exception ex)
            {
                mess = ex.Message;
            }
            return RedirectToAction("Sigup", "Account", new { msg = mess });
        }

        



        //if (ModelState.IsValid)
        //{

        //}
        //else
        //{
        //}



    }
}