using NükEM.Entity;
using NükEM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NükEM.WebApplication.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member

        NükEMEntities ctx = new NükEMEntities();
        // GET: Member
        public ActionResult MemberLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MemberLogin(UserClass uc, string RememberMe)
        {
            bool validationResult = Membership.ValidateUser(uc.UserName, uc.Password);
            if (validationResult == true)
            {
                //Girilen kullanıcı ve şifre bilgileri doğru ise kullanıcının web sitemize giriş yapabilmesi gerekir
                //bunun için öncelikle web.config de authorization ayarlarının yapılması gerekir. ayarlar yapıldıktan sora FormsAuthentication.RedirectFromLoginPage() metodu çağrılacaktır
                if (RememberMe == "on")
                {
                    FormsAuthentication.RedirectFromLoginPage(uc.UserName, true);
                }
                else
                {
                    FormsAuthentication.RedirectFromLoginPage(uc.UserName, false);
                }

            }
            else
            {
                ViewBag.Message = "Kullanıcı adı/parola hatalı.";
            }
            return View();
        }

        public ActionResult MemberLogout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("MemberLogin");
        }
    }
}