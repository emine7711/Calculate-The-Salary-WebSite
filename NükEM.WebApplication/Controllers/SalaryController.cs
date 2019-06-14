//using NükEM.Entity;
using NükEM.Entity;
using NükEM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NükEM.WebApplication.Controllers
{
    public class SalaryController : Controller
    {
        NükEMEntities _context = new NükEMEntities();
        // GET: Salary
        public ActionResult SalaryView()
        {
            string viewFrom = Request.QueryString["vwFrm"].ToString();
            string salaryType = Request.QueryString["slryType"].ToString();
            ViewBag.viewFrom = viewFrom;
            ViewBag.salaryType = salaryType;


            return View();
        }
        [HttpPost]
        public ActionResult SalaryResult(SalaryView sw)
        {
            double IYPO = _context.SalaryCalculationConstants.Where(x => x.SCCCode == "IYPO").Select(x => x.SCCRatio).FirstOrDefault();
            double IKVSPO = _context.SalaryCalculationConstants.Where(x => x.SCCCode == "IKVSPO").Select(x => x.SCCRatio).FirstOrDefault();
            double IGSSPO = _context.SalaryCalculationConstants.Where(x => x.SCCCode == "IGSSPO").Select(x => x.SCCRatio).FirstOrDefault();
            double IISO = _context.SalaryCalculationConstants.Where(x => x.SCCCode == "IISO").Select(x => x.SCCRatio).FirstOrDefault();
            double IDVO = _context.SalaryCalculationConstants.Where(x => x.SCCCode == "IDVO").Select(x => x.SCCRatio).FirstOrDefault();

            Calculate calculation = new Calculate();
            double insurance = calculation.EmployeeInsurance(sw.rawSalary, IYPO, IKVSPO, IGSSPO, IISO);

            string disability = sw.disability;
            string married = sw.married;
            string spouseWork = sw.spouseWork;
            string retired = sw.retired;
            int numberOfChildren = sw.numberOfChildren;

            List<double> resultList = new List<double>();
            if (sw.hiddenBag == "Brüt")
            {
                for (int numberOfMonths = 1; numberOfMonths <= 12; numberOfMonths++)
                {
                    double salary = calculation.GrossToNet(sw.rawSalary, IYPO, IKVSPO, IGSSPO, IISO, numberOfMonths, IDVO, disability, married, spouseWork, retired, numberOfChildren);
                    resultList.Add(salary);
                }
            }
            else
            {
                for (int numberOfMonths = 1; numberOfMonths <= 12; numberOfMonths++)
                {
                    double salary = calculation.NetToGross(sw.rawSalary, IYPO, IKVSPO, IGSSPO, IISO, numberOfMonths, IDVO, disability, married, spouseWork, retired, numberOfChildren);
                    resultList.Add(salary);
                }
            }
            ViewBag.resultList = resultList;
            ViewBag.rawSalary = sw.rawSalary;
            ViewBag.hiddenBag = sw.hiddenBag;
            return View();

        }
        [HttpPost]
        public ActionResult SalaryTable(SalaryResult sr)
        {
            Calculate calculation = new Calculate();
            double IYPO = _context.SalaryCalculationConstants.Where(x => x.SCCCode == "IYPO").Select(x => x.SCCRatio).FirstOrDefault();
            double IKVSPO = _context.SalaryCalculationConstants.Where(x => x.SCCCode == "IKVSPO").Select(x => x.SCCRatio).FirstOrDefault();
            double IGSSPO = _context.SalaryCalculationConstants.Where(x => x.SCCCode == "IGSSPO").Select(x => x.SCCRatio).FirstOrDefault();
            double IISO = _context.SalaryCalculationConstants.Where(x => x.SCCCode == "IISO").Select(x => x.SCCRatio).FirstOrDefault();
            double IDVO = _context.SalaryCalculationConstants.Where(x => x.SCCCode == "IDVO").Select(x => x.SCCRatio).FirstOrDefault();

            string disability = "";
            string married = "";
            string spouseWork = "";
            string retired = "";
            int numberOfChildren = 0;

            List<SalaryResult> resultList = new List<SalaryResult>();
            double insurance = calculation.EmployeeInsurance(sr.rawSalary, IYPO, IKVSPO, IGSSPO, IISO);
            double stampTax = calculation.StampTax(sr.rawSalary, IDVO);
            double minLivingAllow = calculation.MinLivingAllowance(married, spouseWork, numberOfChildren);
            if (sr.hiddenBag == "Brüt")
            {
                for (int numberOfMonths = 1; numberOfMonths <= 12; numberOfMonths++)
                {

                    double cita = calculation.CITA(sr.rawSalary, insurance, numberOfMonths);
                    double mita = calculation.MITA(cita, numberOfMonths);
                    double resultSalary = calculation.GrossToNet(sr.rawSalary, IYPO, IKVSPO, IGSSPO, IISO, numberOfMonths, IDVO, disability, married, spouseWork, retired, numberOfChildren);

                    resultList.Add(new SalaryResult()
                    {
                        rawSalary = sr.rawSalary,
                        insurance = insurance,
                        mita = mita,
                        stampTax = stampTax,
                        cita = cita,
                        resultSalary = resultSalary,
                        minLivingAllow = minLivingAllow
                    });
                }
            }
            else
            {
                for (int numberOfMonths = 1; numberOfMonths <= 12; numberOfMonths++)
                {

                    double cita = calculation.CITA(sr.rawSalary, insurance, numberOfMonths);
                    double mita = calculation.MITA(cita, numberOfMonths);
                    double resultSalary = calculation.NetToGross(sr.rawSalary, IYPO, IKVSPO, IGSSPO, IISO, numberOfMonths, IDVO, disability, married, spouseWork, retired, numberOfChildren);

                    resultList.Add(new SalaryResult()
                    {
                        resultSalary = resultSalary,
                        insurance = insurance,
                        mita = mita,
                        stampTax = stampTax,
                        cita = cita,
                        rawSalary = sr.rawSalary,
                        minLivingAllow = minLivingAllow
                    });
                }
            }
            ViewBag.resultList = resultList;
            ViewBag.hiddenBag = sr.hiddenBag;
            return View();
        }

    }
}