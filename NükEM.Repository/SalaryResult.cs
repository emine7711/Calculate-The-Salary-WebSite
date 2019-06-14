using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NükEM.Repository
{
    public class SalaryResult
    {
        public double rawSalary { get; set; }
        public double insurance { get; set; }
        public double mita { get; set; }
        public double stampTax { get; set; }
        public double cita { get; set; }
        public double resultSalary { get; set; }
        public double minLivingAllow { get; set; }
        public string hiddenBag { get; set; }
    }
}
