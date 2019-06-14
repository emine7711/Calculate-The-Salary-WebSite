using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NükEM.WebApplication.Controllers
{
    public class RoleController : Controller
    {
        // GET: Role
        public ActionResult Index()
        {
            List<string> roles = Roles.GetAllRoles().ToList();
            return View(roles);
        }
        public ActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddRole(string rolename)
        {
            Roles.CreateRole(rolename);
            return RedirectToAction("Index");
        }
    }
}