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
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            MembershipUserCollection users = Membership.GetAllUsers();
            return View(users);
        }
        public ActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddUser(UserClass uc)
        {
            Membership.CreateUser(uc.UserName, uc.Password, uc.Email, uc.PasswordQuestion, uc.PasswordAnswer, true, out MembershipCreateStatus status);
            string createMessage = "";
            switch (status)
            {
                case MembershipCreateStatus.Success:
                    break;
                case MembershipCreateStatus.InvalidUserName:
                    createMessage = "Geçersiz kullanıcı adı.";
                    break;
                case MembershipCreateStatus.InvalidPassword:
                    createMessage = "Geçersiz parola";
                    break;
                case MembershipCreateStatus.InvalidEmail:
                    createMessage = "Geçersiz e-mail";
                    break;
                case MembershipCreateStatus.DuplicateUserName:
                    createMessage = "Kullanılmış kullanıcı adı";
                    break;
                case MembershipCreateStatus.DuplicateEmail:
                    createMessage = "Kullanılmış email";
                    break;
                case MembershipCreateStatus.UserRejected:
                    createMessage = "Kullanıcı engellendi";
                    break;
                case MembershipCreateStatus.ProviderError:
                    createMessage = "Sağlayıcı hatası";
                    break;
                case MembershipCreateStatus.InvalidQuestion:
                    createMessage = "Geçersili gizli soru";
                    break;
                case MembershipCreateStatus.InvalidAnswer:
                    createMessage = "Geçersiz gizli cevap";
                    break;
                case MembershipCreateStatus.InvalidProviderUserKey:
                    createMessage = "Geçersiz kullanıcı anahtarı";
                    break;
                case MembershipCreateStatus.DuplicateProviderUserKey:
                    createMessage = "Kullanılmmış kullanıcı anahtarı";
                    break;
                default:
                    break;
            }

            ViewBag.createMessage = createMessage;
            if (createMessage=="")
            {
                
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        public ActionResult UserInRoles(string username, string message = null)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return RedirectToAction("Index");
            }
            MembershipUser user = Membership.GetUser(username);
            if (user == null)
            {
                return HttpNotFound();
            }
            string[] userRoles = Roles.GetRolesForUser(username);
            string[] allRoles = Roles.GetAllRoles();
            List<string> availableRoles = new List<string>();
            foreach (string role in allRoles)
            {
                if (userRoles.Contains(role) == false)
                {
                    availableRoles.Add(role);
                }
            }
            ViewBag.AvailableRoles = availableRoles;
            ViewBag.UserRoles = userRoles;
            ViewBag.Username = username;
            ViewBag.Message = message;
            return View();
        }
        [HttpPost]
        public ActionResult UserInRoles(string username, List<string> availableRoles)
        {
            if (availableRoles == null)
                return RedirectToAction("UserInRoles", new { username = username, message = "Önce rol seçiniz" });

            if (availableRoles.Count < 1)
                return RedirectToAction("UserInRoles", new { username = username, message = "Hata" });

            Roles.AddUserToRoles(username, availableRoles.ToArray());
            return RedirectToAction("UserInRoles", new { username = username, message = "Başarılı" });
        }
     
    }
}