using System;

namespace Qtc.Branch.BusinessEntities
{
	public class DeliveryScheduleCriteria : DeliverySchedule
	{
        public DateTime mDeliveryStartDate { get; set; }
        public DateTime mDeliveryEndDate { get; set; }
    }
}