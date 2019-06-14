using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NükEM.Repository
{
    public class SalaryView
    {
        public double rawSalary { get; set; }
        
        public int year { get; set; }
        public string disability { get; set; }
        public string married { get; set; }
        public string spouseWork { get; set; }
        public string retired { get; set; }
        public int numberOfChildren { get; set; }
        public string hiddenBag { get; set; }
    }
}
