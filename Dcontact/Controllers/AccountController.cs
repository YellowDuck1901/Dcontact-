using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Util;

namespace Dcontact.Controllers
{
    [HandleError]
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Comfirm(String msg)
        {
            //co typechange
            ViewBag.msg = msg;
            return View();
        }

        public ActionResult ComfirmForm()
        {
            String mess = "";
            string vertifyCode = "";
            vertifyCode = Request.QueryString["vertifyCode"];
            try
            {
                if (vertifyCode.Equals((string)Session[(string)Session["email"]]))
                {
                    Session.Add("AvaiableChangePassword", true);  //nhap lai email thi phai set value false

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
            return RedirectToAction("Comfirm", "Account", new { msg = mess });
        }

        public ActionResult CreateNewPasswordForm()
        {
            String mess = "";
            string password = Request.QueryString["password"];
            Bean.User user = (Bean.User)Session["user"];

            try
            {
                if ((bool)Session["AvaiableChangePassword"])
                {
                    Util.DAO d = new Util.DAO();
                    if (d.DB_ChangePass((string)Session["email"], password))
                    {
                        Session["AvaiableChangePassword"] = false;  //remove value cho phep doi password
                        Session.Remove((string)Session[(string)Session["email"]]);  //xoa cap value email
                        Session.Remove((string)Session["email"]);
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        mess = "Wrong Verification Code";
                    }
                }
                else
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
                    if (d.DB_IsAdmin(username))
                    {
                        //getAdmin
                        Bean.User admin = d.DB_getAdmin(username);
                        Session.Add("user", admin);
                        return RedirectToAction("admin", "Admin");
                    }
                    else
                    {
                        //getuser
                        Bean.User user = d.DB_getUser(username);   //khoi tao object user voi data từ db
                        Session.Add("user", user);                                          // ("key","object")
                        return RedirectToAction("dashboard", "DcontactAndDcrad");
                    }
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
            return RedirectToAction("Login", "Account", new { msg = mess });
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

        public ActionResult ChangeEmailForm()
        {
            string mess = "";
            string email = Request.QueryString["email"];
            Bean.User user = (Bean.User)Session["user"];

            bool avaiable = (bool)Session["AvaiableChangeEmail"];   //nhap lai email thi set false

            try
            {
                if (avaiable)
                {
                    Session.Add("AvaiableChangeEmail", false);

                    Util.DAO d = new Util.DAO();
                    var vertifyCode = RandomCode.Random_6D();
                    Mail.send(email, "Code to Change Email", vertifyCode);

                    //xoa đi email và vertifycode cũ trên session
                    Session.Remove((string)Session[(string)Session["email"]]);  //xoa cap value email
                    Session.Remove((string)Session["email"]);

                    //add new email và vertifycode mới
                    Session.Add("NewEmail", email);            //session luu tru email
                    Session.Add("" + email, vertifyCode);   //key la email con du lieu tren session cua email la vertifycode

                    return RedirectToAction("ComfirmNewEmail", "Account", new { msg = mess });

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
            return RedirectToAction("ChangeEmail", "Account", new { msg = mess });
        }

        public ActionResult ChangeEmail(String msg)
        {
            ViewBag.msg = msg;
            return View();
        }

        public ActionResult ComfirmLogged(String msg)
        {
            Bean.User user = (Bean.User)Session["user"];
            String mess = "";

            //mess = username + "@" + password;
            try
            {
                if ((string)Session["email"] == null)
                {
                    Util.DAO d = new Util.DAO();
                    var vertifyCode = RandomCode.Random_6D();
                    //truong hop back nhap lai email
                    Session.Add("email", user.email);            //session luu tru email
                    Session.Add("" + user.email, vertifyCode);   //key la email con du lieu tren session cua email la vertifycode
                    Session.Add("AvaiableChangeEmail", false);   //nhap lai email thi set false
                    Mail.send(user.email, "Code to Change Email", vertifyCode);
                    ViewBag.msg = msg;
                    return View();
                }
            }
            catch (Exception ex)
            {
                mess = ex.Message;
            }
            ViewBag.msg = msg;
            return View();

        }

        public ActionResult ComfirmLoggedForm(String msg)
        {
            String mess = "";
            string vertifyCode = "";
            vertifyCode = Request.QueryString["vertifyCode"];
            try
            {
                if (vertifyCode.Equals((string)Session[(string)Session["email"]]))
                {
                    Session.Add("AvaiableChangeEmail", true);  //nhap lai email thi phai set value false

                    return RedirectToAction("ChangeEmail", "Account");
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
            return RedirectToAction("ComfirmLogged", "Account", new { msg = mess });
        }

        public ActionResult ComfirmNewEmail(String msg)
        {
            ViewBag.msg = msg;
            return View();
        }

        public ActionResult ComfirmNewEmailForm()
        {
            String mess = "";
            string vertifyCode = "";
            vertifyCode = Request.QueryString["vertifyCode"];
            try
            {
                Util.DAO d = new Util.DAO();
                Bean.User user = (Bean.User)Session["user"];

                if (vertifyCode.Equals((string)Session[(string)Session["NewEmail"]]))
                {
                    if (d.DB_UpdateProfile(user.id, (string)Session["NewEmail"]))
                    {
                        user.email = (string)Session["NewEmail"]; //set email cho user  

                        Session.Clear();
                        Session.Add("user", user);                // add user da update profile lên session



                        return RedirectToAction("dashboard", "DcontactAndDcrad");
                    }
                    else
                    {
                        mess = "You must login !";

                    }
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
            return RedirectToAction("ComfirmNewEmail", "Account", new { msg = mess });
        }

    }
}