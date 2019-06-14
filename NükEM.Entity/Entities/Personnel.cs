using NükEM.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NükEM.Entity
{
    public class Personnel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PersonnelId { get; set; }
        
        public string Name { get; set; }
        public string Surname { get; set; }

        public string Department { get; set; }
        public DateTime? RecruitmentDate { get; set; }
        public int? TimeInTitle { get; set; }

      

        public int TitleId { get; set; }
        public virtual Title title { get; set; }
        [Required]
        public virtual PersonnelDetail PersonnelDetail { get; set; }

    }
}
