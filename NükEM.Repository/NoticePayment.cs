using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NükEM.Repository
{
    public class NoticePayment
    {
        public int numberOfWorkDay { get; set; }
        public double grossPayment { get; set; }
        public double stampTax { get; set; }
        public double mit { get; set; }
        public double netPayment { get; set; }
        public string thead1 { get; set; }
        public string thead2 { get; set; }
        public string thead3 { get; set; }
        public string thead { get; set; }
        public string thead4 { get; set; }
    }
}
