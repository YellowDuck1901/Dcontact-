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
        public ActionResult Comfirm(String msg)
        {
            ViewBag.msg = msg;
            return View();
        }
        public ActionResult ComfirmForm()
        {
            String mess = "";
            string vertifyCode = Request.QueryString["vertifyCode"];
            try
            {
                if (vertifyCode.Equals((string)Session[(string)Session["email"]])){
                    Session.Add("AvaiableChangePassword", true);  //nhap lai email thi phai set value false
                    Session.Remove((string)Session[(string)Session["email"]]);  //xoa cap value email
                    Session.Remove((string)Session["email"]);
                    return RedirectToAction("CreateNewPassword", "Account");
                }
                else
                {
                    mess = "Your verification code has expired";
                }
            }
            catch (Exception ex)
            {
                mess = ex.Message;
            }
            return RedirectToAction("RecoverPassword", "Account",new {msg =mess});
        }

        public ActionResult CreateNewPasswordForm()
        {
            String mess = "";
            string password = Request.QueryString["password"];
            Bean.User user = (Bean.User)Session["user"];  

                try
                {
                if (user != null)
                {
                    Util.DAO d = new Util.DAO();
                    if (d.DB_ChangePass(user.email, password))
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    /*else
                    {
                        mess = "Wrong Verification Code";
                    }*/
                }
                else if ((bool)Session["AvaiableChangePassword"])
                    {
                        Util.DAO d = new Util.DAO();
                        if (d.DB_ChangePass((string)Session["email"], password))
                        {
                            Session.Remove("AvaiableChangePassword");  //remove value cho phep doi password
                            return RedirectToAction("Login", "Account");
                        }
                        else
                        {
                        mess = "Wrong Verification Code";
                        }
                    }else
                        {
                            mess = "Your Verification Code Has Expired";
                        }
               

                }
                catch (Exception ex)
                {
                    mess = ex.Message;
                }
            return RedirectToAction("CreateNewPassword", "Account", new { msg = mess });


        }

        public ActionResult CreateNewPassword(String msg)
        {
            ViewBag.msg = msg;
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
            return RedirectToAction("Login", "Account", new { msg = mess });
        }



        public ActionResult RecoverPasswordForm()
        {
            String mess = "";
            string email = Request.QueryString["email"];
            //mess = username + "@" + password;
            try
            {
                Util.DAO d = new Util.DAO();
                if (d.DB_checkExistedEmail(email))
                {
                    var vertifyCode = RandomCode.Random_6D();
                    //truong hop back nhap lai email
                    Session.Add("email", email);            //session luu tru email
                    Session.Add("" + email, vertifyCode);   //key la email con du lieu tren session cua email la vertifycode
                    Session.Add("AvaiableChangePassword", false);   //nhap lai email thi set false
                                                                    //set time out cho code 
                    Mail.send(email, "Code to Change Password", vertifyCode);
                    return RedirectToAction("Comfirm", "Account");
                }
                else
                {
                    mess = "Account with this email has not be register";
                }
               
            }
            catch (Exception ex)
            {
                mess = ex.Message;
            }
            return RedirectToAction("RecoverPassword", "Account",new {msg = mess});
        }
    
        public ActionResult RecoverPassword(String msg)
        {
            ViewBag.msg = msg;
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