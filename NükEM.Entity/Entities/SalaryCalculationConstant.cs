using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NükEM.Entity.Entities
{
    public class SalaryCalculationConstant
    {
        
        public int Id { get; set; }
        public string SCCCode { get; set; }
        public string Description { get; set; }
        public double SCCRatio { get; set; }
    }
}
