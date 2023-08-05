using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class DeliveryScheduleAudit
	{
		public static AuditCollection Audit(DeliverySchedule deliveryschedule,DeliverySchedule deliveryscheduleOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (deliveryschedule.mLmmId != deliveryscheduleOld.mLmmId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deliveryschedule);
				audit.mField = "lmm_id";
				audit.mOldValue = deliveryscheduleOld.mLmmId.ToString();
				audit.mNewValue = deliveryschedule.mLmmId.ToString();
				audit_collection.Add(audit);
			}

			if (deliveryschedule.mDeliveryScheduleId != deliveryscheduleOld.mDeliveryScheduleId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deliveryschedule);
				audit.mField = "delivery_schedule_id";
				audit.mOldValue = deliveryscheduleOld.mDeliveryScheduleId.ToString();
				audit.mNewValue = deliveryschedule.mDeliveryScheduleId.ToString();
				audit_collection.Add(audit);
			}

			if (deliveryschedule.mBranchId != deliveryscheduleOld.mBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deliveryschedule);
				audit.mField = "branch_id";
				audit.mOldValue = deliveryscheduleOld.mBranchId.ToString();
				audit.mNewValue = deliveryschedule.mBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (deliveryschedule.mEta != deliveryscheduleOld.mEta)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deliveryschedule);
				audit.mField = "eta";
				audit.mOldValue = deliveryscheduleOld.mEta.ToString();
				audit.mNewValue = deliveryschedule.mEta.ToString();
				audit_collection.Add(audit);
			}

			if (deliveryschedule.mDeliveryDate != deliveryscheduleOld.mDeliveryDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deliveryschedule);
				audit.mField = "delivery_date";
				audit.mOldValue = deliveryscheduleOld.mDeliveryDate.ToString();
				audit.mNewValue = deliveryschedule.mDeliveryDate.ToString();
				audit_collection.Add(audit);
			}

			if (deliveryschedule.mDeliveryTime != deliveryscheduleOld.mDeliveryTime)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deliveryschedule);
				audit.mField = "delivery_time";
				audit.mOldValue = deliveryscheduleOld.mDeliveryTime.ToString();
				audit.mNewValue = deliveryschedule.mDeliveryTime.ToString();
				audit_collection.Add(audit);
			}

			if (deliveryschedule.mRecordId != deliveryscheduleOld.mRecordId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deliveryschedule);
				audit.mField = "record_id";
				audit.mOldValue = deliveryscheduleOld.mRecordId.ToString();
				audit.mNewValue = deliveryschedule.mRecordId.ToString();
				audit_collection.Add(audit);
			}

			if (deliveryschedule.mDatetime != deliveryscheduleOld.mDatetime)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deliveryschedule);
				audit.mField = "datetime";
				audit.mOldValue = deliveryscheduleOld.mDatetime.ToString();
				audit.mNewValue = deliveryschedule.mDatetime.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, DeliverySchedule deliveryschedule)
		{
			audit.mUserFullName = deliveryschedule.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_DeliverySchedule);
			audit.mRowId = deliveryschedule.mId;
			audit.mAction = 2;
		}
	}
}