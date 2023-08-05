using System;

namespace Qtc.Branch.BusinessEntities
{
	public class EmployeeCriteria : Employee
	{
        public string mUsername { get; set; }
        public DateTime mSalesScheduleDate { get; set; }
    }
}