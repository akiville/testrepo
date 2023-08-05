using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class DeliveryScheduleConcernAudit
	{
		public static AuditCollection Audit(DeliveryScheduleConcern deliveryscheduleconcern,DeliveryScheduleConcern deliveryscheduleconcernOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (deliveryscheduleconcern.mDeliveryScheduleId != deliveryscheduleconcernOld.mDeliveryScheduleId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deliveryscheduleconcern);
				audit.mField = "delivery_schedule_id";
				audit.mOldValue = deliveryscheduleconcernOld.mDeliveryScheduleId.ToString();
				audit.mNewValue = deliveryscheduleconcern.mDeliveryScheduleId.ToString();
				audit_collection.Add(audit);
			}

			if (deliveryscheduleconcern.mExplanation != deliveryscheduleconcernOld.mExplanation)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deliveryscheduleconcern);
				audit.mField = "explanation";
				audit.mOldValue = deliveryscheduleconcernOld.mExplanation;
				audit.mNewValue = deliveryscheduleconcern.mExplanation;
				audit_collection.Add(audit);
			}

			if (deliveryscheduleconcern.mDeliveryDate != deliveryscheduleconcernOld.mDeliveryDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deliveryscheduleconcern);
				audit.mField = "delivery_date";
				audit.mOldValue = deliveryscheduleconcernOld.mDeliveryDate.ToString();
				audit.mNewValue = deliveryscheduleconcern.mDeliveryDate.ToString();
				audit_collection.Add(audit);
			}

			if (deliveryscheduleconcern.mLmmId != deliveryscheduleconcernOld.mLmmId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deliveryscheduleconcern);
				audit.mField = "lmm_id";
				audit.mOldValue = deliveryscheduleconcernOld.mLmmId.ToString();
				audit.mNewValue = deliveryscheduleconcern.mLmmId.ToString();
				audit_collection.Add(audit);
			}

			if (deliveryscheduleconcern.mReportDate != deliveryscheduleconcernOld.mReportDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deliveryscheduleconcern);
				audit.mField = "report_date";
				audit.mOldValue = deliveryscheduleconcernOld.mReportDate.ToString();
				audit.mNewValue = deliveryscheduleconcern.mReportDate.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, DeliveryScheduleConcern deliveryscheduleconcern)
		{
			audit.mUserFullName = deliveryscheduleconcern.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_DeliveryScheduleConcern);
			audit.mRowId = deliveryscheduleconcern.mId;
			audit.mAction = 2;
		}
	}
}