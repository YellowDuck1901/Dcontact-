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
            var typefunction = "";
            typefunction =  (string)Session["function"];
            if(typefunction.Equals("VertifyCodeCurrentEmail"))
            {
                ViewBag.Title = "Verification Code Has Been Sent to Email";
            }else if(typefunction.Equals("VertifyCodeNewEmail"))
            {
                ViewBag.Title = "Verification Code Has Been Sent to New Email";
            }
            else
            {
                ViewBag.Title = "Verification Code Has Been Sent to Email";
            }
            //co typechange
            ViewBag.msg = msg;
            return View();
        }

        public ActionResult Identifyfunction(string function)
        {
            Bean.User user = (Bean.User)Session["user"];

            if (function.Equals("VertifyCodeCurrentEmail")){
                var vertifyCode = RandomCode.Random_6D();
                Session.Add("email", user.email);            //session luu tru email
                Session.Add("" + user.email, vertifyCode);   //key la email con du lieu tren session cua email la vertifycode
                Session.Add("function", function);
                Session.Add("VerifyCodeExpiry", true);

                Mail.send(user.email, "Code to Verify Email", vertifyCode);
                return RedirectToAction("Comfirm", "Account");
            }
            return RedirectToAction("Comfirm", "Account");
        }

        public ActionResult ComfirmForm()
        {
            String mess = "";
            string vertifyCode = "";
            Util.DAO d = new Util.DAO();
            Bean.User user = (Bean.User)Session["user"];

            vertifyCode = Request.QueryString["vertifyCode"];
            try
            {

                if ((string)Session["function"] == "RecoverPasswordNotLogin")            //recover password not login
                {
                    if (vertifyCode.Equals((string)Session[(string)Session["email"]]))
                    {
                        return RedirectToAction("CreateNewPassword", "Account");

                    }
                    else if(!(bool)Session["VerifyCodeExpiry"])                          //code hết hạn
                    {
                        mess = "Verification Code Has Expired,type your email to get a new code";
                        return RedirectToAction("RecoverPassword", "Account", new { msg = mess });
                    }
                    else if(!vertifyCode.Equals((string)Session[(string)Session["email"]]))                                                     //mã nhập sai
                    {
                        mess = "The code is wrong !";
                        return RedirectToAction("Comfirm", "Account", new { msg = mess });
                    }
                }else if ((string)Session["function"] == "VertifyCodeCurrentEmail")       //verify current email
                {
                    if (user != null)
                    {
                        if (vertifyCode.Equals((string)Session[(string)Session["email"]]))
                        {
                            Session["VerifyCodeExpiry"] = false;  //remove value cho phep doi password
                            Session.Remove((string)Session[(string)Session["email"]]);  //xoa cap value email
                            Session.Remove((string)Session["email"]);
                            return RedirectToAction("ChangeEmail", "Account");

                        }
                        else if (!(bool)Session["VerifyCodeExpiry"])                          //code hết hạn
                        {
                            mess = "Verification Code Has Expired";
                            return RedirectToAction("comfirm", "Account", new { msg = mess });
                        }
                        else if (!vertifyCode.Equals((string)Session[(string)Session["email"]]))                                                     //mã nhập sai
                        {
                            mess = "The code is wrong !";
                            return RedirectToAction("Comfirm", "Account", new { msg = mess });
                        }
                    }
                    else
                    {
                        mess = "Please login agian !";
                        return RedirectToAction("Login", "Account", new { msg = mess });

                    }

                }
                else if ((string)Session["function"] == "VertifyCodeNewEmail")       //verify new email
                {
                    if(user != null)
                    {
                        if (vertifyCode.Equals((string)Session[(string)Session["email"]]))
                        {
                            d.DB_UpdateProfile(user.id, (string)Session["email"]);
                            user.email = (string)Session["email"];
                            Session.Add("user", user);
                            Session["VerifyCodeExpiry"] = false;  //remove value cho phep doi password
                            Session.Remove((string)Session[(string)Session["email"]]);  //xoa cap value email
                            Session.Remove((string)Session["email"]);
                            return RedirectToAction("dashboard", "DcontactAndDcrad");

                        }
                        else if (!(bool)Session["VerifyCodeExpiry"])                          //code hết hạn
                        {
                            mess = "Verification Code Has Expired";
                            return RedirectToAction("Comfirm", "Account", new { msg = mess });
                        }
                        else if (!vertifyCode.Equals((string)Session[(string)Session["email"]]))                                                     //mã nhập sai
                        {
                            mess = "The code is wrong !";
                            return RedirectToAction("Comfirm", "Account", new { msg = mess });
                        }
                    }
                    else
                    {
                        mess = "Please login agian !";
                         return RedirectToAction("Login", "Account", new { msg = mess });

                    }

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
            Util.DAO d = new Util.DAO();

            try
            {
                if (user != null)               //logged 
                {
                    d.DB_ChangePass(user.email, password);
                    Session["VerifyCodeExpiry"] = false;  //remove value cho phep doi password
                    Session.Remove((string)Session[(string)Session["email"]]);  //xoa cap value email
                    Session.Remove((string)Session["email"]);
                    return RedirectToAction("dashboard", "DcontactAndDcrad");
                }
                else if ((bool)Session["VerifyCodeExpiry"])  //not login
                {
                    if (d.DB_ChangePass((string)Session["email"], password))
                    {
                        Session["VerifyCodeExpiry"] = false;  //remove value cho phep doi password
                        Session.Remove((string)Session[(string)Session["email"]]);  //xoa cap value email
                        Session.Remove((string)Session["email"]);
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        mess = "Some thing wrong";
                    }
                }
                else
                {
                    mess = "Your Verification Code Has Expired";
                    return RedirectToAction("RecoverPassword", "Account", new { msg = mess });
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
                    Session.Add("function", "RecoverPasswordNotLogin");   
                    Session.Add("VerifyCodeExpiry", true);   

                    Mail.send(email, "Code to Change Password", vertifyCode);
                    return RedirectToAction("Comfirm", "Account");
                }
                else
                {
                  
                }

            }
            catch (Exception ex)
            {
                mess = ex.Message;
            }
            mess = "Account with this email has not be register";
            return RedirectToAction("RecoverPassword", "Account", new { msg = mess });
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
            Util.DAO d = new Util.DAO();

            try
            {
              if(user != null)
                {
                    var vertifyCode = RandomCode.Random_6D();
                    //truong hop back nhap lai email
                    Session.Add("email", email);            //session luu tru email
                    Session.Add("" + email, vertifyCode);   //key la email con du lieu tren session cua email la vertifycode
                    Session.Add("function", "VertifyCodeNewEmail");
                    Session.Add("VerifyCodeExpiry", true);

                    Mail.send(email, "Code to Verify New Email", vertifyCode);
                    return RedirectToAction("Comfirm", "Account");
                }
                else
                {
                    mess = "Your must login agian !";
                    return RedirectToAction("ChangeEmail", "Account", new { msg = mess });
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
    }
}