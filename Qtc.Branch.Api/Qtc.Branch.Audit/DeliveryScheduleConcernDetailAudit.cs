using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class DeliveryScheduleConcernDetailAudit
	{
		public static AuditCollection Audit(DeliveryScheduleConcernDetail deliveryscheduleconcerndetail,DeliveryScheduleConcernDetail deliveryscheduleconcerndetailOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (deliveryscheduleconcerndetail.mDeliveryScheduleConcernId != deliveryscheduleconcerndetailOld.mDeliveryScheduleConcernId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deliveryscheduleconcerndetail);
				audit.mField = "delivery_schedule_concern_id";
				audit.mOldValue = deliveryscheduleconcerndetailOld.mDeliveryScheduleConcernId.ToString();
				audit.mNewValue = deliveryscheduleconcerndetail.mDeliveryScheduleConcernId.ToString();
				audit_collection.Add(audit);
			}

			if (deliveryscheduleconcerndetail.mDeliveryScheduleDetailId != deliveryscheduleconcerndetailOld.mDeliveryScheduleDetailId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deliveryscheduleconcerndetail);
				audit.mField = "delivery_schedule_detail_id";
				audit.mOldValue = deliveryscheduleconcerndetailOld.mDeliveryScheduleDetailId.ToString();
				audit.mNewValue = deliveryscheduleconcerndetail.mDeliveryScheduleDetailId.ToString();
				audit_collection.Add(audit);
			}

			if (deliveryscheduleconcerndetail.mRemarks != deliveryscheduleconcerndetailOld.mRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deliveryscheduleconcerndetail);
				audit.mField = "remarks";
				audit.mOldValue = deliveryscheduleconcerndetailOld.mRemarks;
				audit.mNewValue = deliveryscheduleconcerndetail.mRemarks;
				audit_collection.Add(audit);
			}

			if (deliveryscheduleconcerndetail.mActualItemQtyReceived != deliveryscheduleconcerndetailOld.mActualItemQtyReceived)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deliveryscheduleconcerndetail);
				audit.mField = "actual_item_qty_received";
				audit.mOldValue = deliveryscheduleconcerndetailOld.mActualItemQtyReceived.ToString();
				audit.mNewValue = deliveryscheduleconcerndetail.mActualItemQtyReceived.ToString();
				audit_collection.Add(audit);
			}

			if (deliveryscheduleconcerndetail.mLmmId != deliveryscheduleconcerndetailOld.mLmmId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deliveryscheduleconcerndetail);
				audit.mField = "lmm_id";
				audit.mOldValue = deliveryscheduleconcerndetailOld.mLmmId.ToString();
				audit.mNewValue = deliveryscheduleconcerndetail.mLmmId.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, DeliveryScheduleConcernDetail deliveryscheduleconcerndetail)
		{
			audit.mUserFullName = deliveryscheduleconcerndetail.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_DeliveryScheduleConcernDetail);
			audit.mRowId = deliveryscheduleconcerndetail.mId;
			audit.mAction = 2;
		}
	}
}