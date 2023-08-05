using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Qtc.Branch.Api.Models
{
    public class BranchProductApi
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
    }
}