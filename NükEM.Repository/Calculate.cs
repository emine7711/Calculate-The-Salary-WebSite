using NükEM.Entity;
using NükEM.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NükEM.Repository
{
    public class Calculate
    {
        double insurance = 0;
        public double EmployeeInsurance(double gross, double IYPO, double IKVSPO, double IGSSPO, double IISO)
        {
            insurance = gross * (IYPO + IKVSPO + IGSSPO + IISO);
            return Math.Round(insurance,2);
        }

        double cita = 0;
        public double CITA(double gross, double insurance, int numberOfMonths)
        {
            cita = (gross - insurance) * numberOfMonths;
            return Math.Round(cita,2);
        }

        double mit = 0;
        public double MITA(double cita,int numberOfMonths)
        {
            double monthlySalary = cita / numberOfMonths;
            double previousCita = monthlySalary * (numberOfMonths - 1);
            int min, max;

            NükEMEntities db = new NükEMEntities();
            List<TaxBracket> taxBracket = db.TaxBrackets.ToList();
            int IsOnBorder = 0;
            foreach (TaxBracket item in taxBracket)
            {
                min = item.MinCITA ?? 0;
                max = item.MaxCITA ?? 0;
                if (cita > min && previousCita < min)
                {
                    IsOnBorder = 1;
                }

                if (cita > min && cita < max && IsOnBorder == 0)
                {
                    mit = monthlySalary * item.Bracket;
                    break;
                }
                else if (cita > min && cita < max && IsOnBorder == 1)
                {
                    TaxBracket previous = db.TaxBrackets.Find(item.Id - 1);
                    mit = (cita - min) * item.Bracket + (min - previousCita) * previous.Bracket;
                    break;
                }
            }
            return Math.Round(mit,2);
        }
        double stampTax = 0;
        public double StampTax(double gross,double IDVO)
        {
            stampTax = gross * IDVO;
            return Math.Round(stampTax,2);
        }
        public double MinLivingAllowance(string married, string spouseWork,int numberOfChildren)
        {
            double minLivingAllow = 0;
            if (married=="on")
            {
                if (spouseWork=="off")
                {
                    switch (numberOfChildren)
                    {
                        case 0:
                            minLivingAllow = 230.22;
                            break;
                        case 1:
                            minLivingAllow = 259.00;
                            break;
                        case 2:
                            minLivingAllow = 287.78;
                            break;
                        case 3:
                            minLivingAllow = 326.15;
                            break;
                    }
                }
                else if(spouseWork=="on")
                {
                    switch (numberOfChildren)
                    {
                        case 0:
                            minLivingAllow = 191.85;
                            break;
                        case 1:
                            minLivingAllow = 220.63;
                            break;
                        case 2:
                            minLivingAllow = 249.41;
                            break;
                        case 3:
                            minLivingAllow = 287.98;
                            break;
                        case 4:
                            minLivingAllow = 306.96;
                            break;
                        case 5:
                            minLivingAllow = 326.15;
                            break;
                    }
                }
            }
            else
            {
                minLivingAllow = 191.85;
            }
            return Math.Round(minLivingAllow, 2);
        }
        double salary = 0;
        double payment = 0;
        public double GrossToNet(double gross, double IYPO, double IKVSPO, double IGSSPO, double IISO, int numberOfMonths, double IDVO,string disability, string married,string spouseWork, string retired,int numberOfChildren)//Emeklilik ve engellilik formüle dahil edilecek
        {
            double insurance = EmployeeInsurance(gross, IYPO, IKVSPO, IGSSPO, IISO);
            double cita = CITA(gross, insurance, numberOfMonths);
            double mita = MITA(cita, numberOfMonths);
            double stampTax = StampTax(gross, IDVO);
            double minLivingAllow = MinLivingAllowance(married, spouseWork, numberOfChildren);
            salary = gross- insurance - mita - stampTax;
            return Math.Round(salary,2);
        }
      
        public double NetToGross(double net,double IYPO,double IKVSPO, double IGSSPO, double IISO,int numberOfMonths, double IDVO,string disability,string married, string spouseWork, string retired,int numberOfChildren)
        {
            double constant = 1.57;
            double denemeBrüt = net * constant;
            double denemeNet = GrossToNet(denemeBrüt, IYPO, IKVSPO, IGSSPO, IISO, numberOfMonths, IDVO, disability, married, spouseWork, retired, numberOfChildren);
            double fark = 0;
                
            while (Math.Abs(denemeNet - net) > 1)
            {
                
                if (denemeNet > net)
                {
                    fark = denemeNet - net;
                    denemeBrüt = denemeBrüt - fark * constant;
                }
                else if (denemeNet < net)
                {
                    fark = net - denemeNet;
                    denemeBrüt = denemeBrüt + fark * constant;
                }
                denemeNet = GrossToNet(denemeBrüt, IYPO, IKVSPO, IGSSPO, IISO, numberOfMonths, IDVO, disability, married, spouseWork, retired, numberOfChildren);
            }
            return Math.Round(denemeBrüt,2);
        }
        public double GrossPayment(double gross,int numberOfWorkDay)
        {
            double grossPayment = 0;
            double dailySalary = gross / 30;
            double weeklySalary = dailySalary * 7;
            //            – 6 aydan az çalışanlar için 2 hafta
            //– 6 ay ile 1,5 yıl arası çalışanlar için 4 hafta
            //– 1,5 ile 3 yıl arası çalışanlar için 6 hafta
            //– 3 yıldan fazla çalışanlar için 8 hafta
            if (numberOfWorkDay < 180)
            {
                grossPayment = weeklySalary * 2;
            }
            else if (numberOfWorkDay >= 180 && numberOfWorkDay < 540)
            {
                grossPayment = weeklySalary * 4;
            }
            else if (numberOfWorkDay >= 540 && numberOfWorkDay < 1080)
            {
                grossPayment = weeklySalary * 6;
            }
            else if (numberOfWorkDay >= 1080)
            {
                grossPayment = weeklySalary * 8;
            }
            return Math.Round(grossPayment,2);
        }
        public double NoticePayment(double gross, int numberOfWorkDay,double cita,int numberOfMonths,double IDVO)
        {
            double grossPayment = GrossPayment(gross, numberOfWorkDay);
            double stampTax = StampTax(grossPayment, IDVO);
            double mit = MITA(cita, 1);
            payment = grossPayment - (mit + stampTax);
            return Math.Round(payment,2);
        }
        public double GrossSeverancePayment(double gross,int numberOfWorkDay)
        {
            double fark = numberOfWorkDay % 365;
            int year = numberOfWorkDay / 365;
            double grossPayment = (gross * year) + (gross * year) / 365;
            return Math.Round(grossPayment,2);
        }
        public double SeverancePayment(double grossPayment,double IDVO)
        {
            double stampTax = StampTax(grossPayment, IDVO);
            payment = grossPayment - stampTax;
            return Math.Round(payment,2);
        }

        public double MonthlySalary(double daily)
        {

            double monthly = daily * 30;
            return Math.Round(monthly,2);
        }
    }
}
