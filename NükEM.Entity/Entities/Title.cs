using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NükEM.Entity.Entities
{
    public class Title
    {
        public Title()
        {
            this.personnels = new HashSet<Personnel>();
        }
        
        public int Id { get; set; }
        public string Descriptions { get; set; }
        public double? DailySalary { get; set; }
        public double? AdvancementRiseRate { get; set; }
        public virtual ICollection<Personnel> personnels { get; set; }
    }
}
