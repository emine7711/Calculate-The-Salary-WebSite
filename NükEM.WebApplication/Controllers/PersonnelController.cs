using NükEM.Entity;
using NükEM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NükEM.WebApplication.Controllers
{
    public class PersonnelController : Controller
    {
        NükEMEntities _context = new NükEMEntities();
        // GET: Personnel
        public ActionResult PersonnelList()
        {
            PersonnelRepository repo = new PersonnelRepository();
            List<Personnel> personelList = repo.Listing();
            return View(personelList);
        }
        public ActionResult DetailList(int id)
        {
            PersonnelDetailRepository repo = new PersonnelDetailRepository();
            List<PersonnelDetail> detailList = repo.Listing();
            PersonnelDetail pd=detailList.Where(x => x.PersonnelId == id).FirstOrDefault();
            return View(pd);
        }
        public ActionResult PersonnelIndex(string id)
        {
            //giriş yapan kullanıcının userid sini alıp personneldetaydaki userid kullanıcının useridsine eşit olanın bilgilerini çekmem lazım.
            double daily = (double)_context.Personnels.Select(x => x.title.DailySalary).FirstOrDefault();
            Calculate calculation = new Calculate();
            double monthly = calculation.MonthlySalary(daily);

            string disability = _context.PersonnelDetails.Select(x => x.Disability).FirstOrDefault();
            string maritalstatus = _context.PersonnelDetails.Select(x => x.MaritalStatus).FirstOrDefault();
            string spouseWork = _context.PersonnelDetails.Select(x => x.WorkingStatusOfSpouse).FirstOrDefault();
            
            int numberOfChildren =(int)_context.PersonnelDetails.Select(x => x.NumberOfChildren).FirstOrDefault();

            List<SalaryView> personnelInfo = new List<SalaryView>();
            personnelInfo.Add(new SalaryView()
            {
                rawSalary = monthly,
                disability = disability,
                married = maritalstatus,
                spouseWork = spouseWork,
                numberOfChildren = numberOfChildren
            });
            ViewBag.personnelInfo = personnelInfo;
            return View();
        }
        public ActionResult PersonnelDetails()
        {
            return View();
        }
        public ActionResult PersonnelAdd()
        {
            TitleRepository title = new TitleRepository();
            ViewBag.TitleList = title.Listing();
            return View();
        }
        [HttpPost]
        public ActionResult PersonnelAdd(Personnel p,PersonnelDetail pd)
        {
            
            PersonnelDetailRepository pdRepo = new PersonnelDetailRepository();
           
            pdRepo.Adding(pd);
            PersonnelRepository pRepo = new PersonnelRepository();
            p.PersonnelId = pd.PersonnelId;
            p.PersonnelDetail = pd;
            if (p.PersonnelDetail==null)
            {
                return View();
            }
            pRepo.Adding(p);
            return RedirectToAction("PersonnelList");
        }
    }
}