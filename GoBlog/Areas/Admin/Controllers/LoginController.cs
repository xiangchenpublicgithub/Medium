﻿using GoBlog.Areas.Admin.Models;
using GoBlog.Authentication;
using Microsoft.Web.Mvc;
using System.Web.Mvc;

namespace GoBlog.Areas.Admin.Controllers
{
    [ActionLinkArea("Admin")]
    public class LoginController : Controller
    {
        private readonly IAuthenticator authenticator;

        public LoginController(IAuthenticator authenticator)
        {
            this.authenticator = authenticator;
        }

        public ActionResult Index()
        {
            return authenticator.Authenticated ? RedirectToAdmin() : View();
        }

        public ActionResult Login(CredentialsModel model)
        {
            var authenticated = authenticator.Authenticate(model.Username, model.Password);

            if (authenticated)
            {
                return RedirectToAdmin();
            }

            ModelState.AddModelError("", "Invalid username or password.");
            return View("Index", model);
        }

        public ActionResult Logout()
        {
            authenticator.Logout();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        private ActionResult RedirectToAdmin()
        {
            if (Request.QueryString["ReturnUrl"] != null)
            {
                return Redirect(Request.QueryString["ReturnUrl"]);
            }

            return RedirectToAction("Index", "Admin");
        }
    }
}