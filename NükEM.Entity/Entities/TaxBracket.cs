using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NükEM.Entity.Entities
{
    public class TaxBracket
    {
        
        public int Id { get; set; }
        public int? MinCITA { get; set; }
        public int? MaxCITA { get; set; }
        public double Bracket { get; set; }
    }
}
