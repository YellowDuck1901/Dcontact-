﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

    }
}