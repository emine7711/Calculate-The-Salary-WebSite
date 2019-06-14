using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NükEM.Entity
{
    public class PersonnelDetail
    {
        [Key]
       
        public int PersonnelId { get; set; }
        
        public DateTime? Birthdate { get; set; }
        public string Disability { get; set; }
        public string MaritalStatus { get; set; }
        public string WorkingStatusOfSpouse { get; set; }
        public int? NumberOfChildren { get; set; }
        
        public virtual Personnel Personnel { get; set; }
    }
}
