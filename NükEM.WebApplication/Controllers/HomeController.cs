using NükEM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NükEM.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        NükEMEntities _context = new NükEMEntities();
        public ActionResult Index()
        {

            return View();

        }

    }
}