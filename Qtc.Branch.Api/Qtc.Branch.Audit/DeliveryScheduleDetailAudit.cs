using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class DeliveryScheduleDetailAudit
	{
		public static AuditCollection Audit(DeliveryScheduleDetail deliveryscheduledetail,DeliveryScheduleDetail deliveryscheduledetailOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (deliveryscheduledetail.mProductId != deliveryscheduledetailOld.mProductId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deliveryscheduledetail);
				audit.mField = "product_id";
				audit.mOldValue = deliveryscheduledetailOld.mProductId.ToString();
				audit.mNewValue = deliveryscheduledetail.mProductId.ToString();
				audit_collection.Add(audit);
			}

			if (deliveryscheduledetail.mProduct != deliveryscheduledetailOld.mProduct)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deliveryscheduledetail);
				audit.mField = "product";
				audit.mOldValue = deliveryscheduledetailOld.mProduct;
				audit.mNewValue = deliveryscheduledetail.mProduct;
				audit_collection.Add(audit);
			}

			if (deliveryscheduledetail.mCode != deliveryscheduledetailOld.mCode)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deliveryscheduledetail);
				audit.mField = "code";
				audit.mOldValue = deliveryscheduledetailOld.mCode;
				audit.mNewValue = deliveryscheduledetail.mCode;
				audit_collection.Add(audit);
			}

			if (deliveryscheduledetail.mPlanned != deliveryscheduledetailOld.mPlanned)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deliveryscheduledetail);
				audit.mField = "planned";
				audit.mOldValue = deliveryscheduledetailOld.mPlanned.ToString();
				audit.mNewValue = deliveryscheduledetail.mPlanned.ToString();
				audit_collection.Add(audit);
			}

			if (deliveryscheduledetail.mAdditional != deliveryscheduledetailOld.mAdditional)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deliveryscheduledetail);
				audit.mField = "additional";
				audit.mOldValue = deliveryscheduledetailOld.mAdditional.ToString();
				audit.mNewValue = deliveryscheduledetail.mAdditional.ToString();
				audit_collection.Add(audit);
			}

			if (deliveryscheduledetail.mCancel != deliveryscheduledetailOld.mCancel)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deliveryscheduledetail);
				audit.mField = "cancel";
				audit.mOldValue = deliveryscheduledetailOld.mCancel.ToString();
				audit.mNewValue = deliveryscheduledetail.mCancel.ToString();
				audit_collection.Add(audit);
			}

			if (deliveryscheduledetail.mDdir != deliveryscheduledetailOld.mDdir)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deliveryscheduledetail);
				audit.mField = "ddir";
				audit.mOldValue = deliveryscheduledetailOld.mDdir.ToString();
				audit.mNewValue = deliveryscheduledetail.mDdir.ToString();
				audit_collection.Add(audit);
			}

			if (deliveryscheduledetail.mDatestamp != deliveryscheduledetailOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deliveryscheduledetail);
				audit.mField = "datestamp";
				audit.mOldValue = deliveryscheduledetailOld.mDatestamp.ToString();
				audit.mNewValue = deliveryscheduledetail.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, DeliveryScheduleDetail deliveryscheduledetail)
		{
			audit.mUserFullName = deliveryscheduledetail.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_DeliveryScheduleDetail);
			audit.mRowId = deliveryscheduledetail.mId;
			audit.mAction = 2;
		}
	}
}