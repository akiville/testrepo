using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Qtc.Branch.Api.Models
{
    public class EmployeeAttendanceApi
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int AttendanceId { get; set; }
        public int AttendanceTrackingNo { get; set; }
        public int BranchCode { get; set; }
        public string AttendanceDate { get; set; }
    }
}