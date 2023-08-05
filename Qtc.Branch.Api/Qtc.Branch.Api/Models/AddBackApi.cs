using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Qtc.Branch.Api.Models
{
    public class AddBackApi
    {
        public int id { get; set; }
        public int product_avail_id { get; set; }
        public int branch_id { get; set; }
        public int personnel_id { get; set; }
        public int product_id { get; set; }
        public DateTime sales_date { get; set; }
        public int add_back_qty { get; set; }
        public String add_back_reason { get; set; }
        public String add_back_status { get; set; }
        public int prior_qty { get; set; }
        public int avail_qty { get; set; }
        public int approved_by_id { get; set; }
        public DateTime approval_date { get; set; }
        public String approval_remarks { get; set; }
        

    }
}