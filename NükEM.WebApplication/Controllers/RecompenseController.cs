using NükEM.Entity;
using NükEM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NükEM.WebApplication.Controllers
{
    public class RecompenseController : Controller
    {
        NükEMEntities _context = new NükEMEntities();
        // GET: Recompense
        public ActionResult CompansationView()
        {
            string viewFrom = Request.QueryString["vwFrm"].ToString();
            ViewBag.viewFrom = viewFrom;
            return View();
        }

        [HttpPost]
        public ActionResult NoticePayment(CompansationView cw)
        {
            Calculate calculation = new Calculate();
            TimeSpan dateDiff = cw.date2 - cw.date1;
            int numberOfWorkDay = (int)dateDiff.TotalDays;

            double IDVO = _context.SalaryCalculationConstants.Where(x => x.SCCCode == "IDVO").Select(x => x.SCCRatio).FirstOrDefault();

            List<NoticePayment> payment = new List<NoticePayment>();

            if (cw.hiddenBag =="İhbar")
            {
                double IYPO = _context.SalaryCalculationConstants.Where(x => x.SCCCode == "IYPO").Select(x => x.SCCRatio).FirstOrDefault();
                double IKVSPO = _context.SalaryCalculationConstants.Where(x => x.SCCCode == "IKVSPO").Select(x => x.SCCRatio).FirstOrDefault();
                double IGSSPO = _context.SalaryCalculationConstants.Where(x => x.SCCCode == "IGSSPO").Select(x => x.SCCRatio).FirstOrDefault();
                double IISO = _context.SalaryCalculationConstants.Where(x => x.SCCCode == "IISO").Select(x => x.SCCRatio).FirstOrDefault();
                
                
                double insurance = calculation.EmployeeInsurance(cw.gross, IYPO, IKVSPO, IGSSPO, IISO);
                double cita = cw.cita;
                double mit = calculation.MITA(cita, 1);
                double stampTax = calculation.StampTax(cw.gross, IDVO);
                double grossPayment = calculation.GrossPayment(cw.gross, numberOfWorkDay);
                double netPayment = calculation.NoticePayment(cw.gross, numberOfWorkDay, cita, 1, IDVO);

                
                payment.Add(new NoticePayment()
                {
                    thead1 = "Çalışma Süresi",
                    numberOfWorkDay = numberOfWorkDay,

                    thead2 = "Brüt İhbar Tazminatı",
                    grossPayment = grossPayment,

                    thead3 = "Damga Vergisi",
                    stampTax = stampTax,

                    thead = "Gelir Vergisi",
                    mit = mit,

                    thead4 = "Net İhbar Tazminatı",
                    netPayment = netPayment,
                });

                ViewBag.payment = payment;

                ViewBag.viewFrom = cw.hiddenBag;

                return View();
            }
            else
            {
                double grossPayment= calculation.GrossSeverancePayment(cw.gross, numberOfWorkDay);
                double netPayment = calculation.SeverancePayment(grossPayment, IDVO);
                double stampTax = calculation.StampTax(grossPayment, IDVO);
                

                payment.Add(new NoticePayment()
                {
                    thead1 = "Çalışma Süresi",
                    numberOfWorkDay = numberOfWorkDay,

                    thead2 = "Brüt Kıdem Tazminatı",
                    grossPayment =grossPayment,

                    thead3 = "Damga Vergisi",
                    stampTax =stampTax,

                    thead4 = "Net Kıdem Tazminatı",
                    netPayment =netPayment,
                });
                ViewBag.payment = payment;
                ViewBag.viewFrom = cw.hiddenBag;
                return View();
            }
        }
        public ActionResult SeverencePayment()
        {
            return View();
        }
    }
}