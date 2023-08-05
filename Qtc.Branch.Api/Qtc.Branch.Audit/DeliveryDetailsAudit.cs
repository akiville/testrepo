using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class DeliveryDetailsAudit
	{
		public static AuditCollection Audit(DeliveryDetails deliverydetails,DeliveryDetails deliverydetailsOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (deliverydetails.mDispatchId != deliverydetailsOld.mDispatchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deliverydetails);
				audit.mField = "dispatch_id";
				audit.mOldValue = deliverydetailsOld.mDispatchId.ToString();
				audit.mNewValue = deliverydetails.mDispatchId.ToString();
				audit_collection.Add(audit);
			}

			if (deliverydetails.mProductId != deliverydetailsOld.mProductId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deliverydetails);
				audit.mField = "product_id";
				audit.mOldValue = deliverydetailsOld.mProductId.ToString();
				audit.mNewValue = deliverydetails.mProductId.ToString();
				audit_collection.Add(audit);
			}

			if (deliverydetails.mProduct != deliverydetailsOld.mProduct)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deliverydetails);
				audit.mField = "product";
				audit.mOldValue = deliverydetailsOld.mProduct;
				audit.mNewValue = deliverydetails.mProduct;
				audit_collection.Add(audit);
			}

			if (deliverydetails.mCode != deliverydetailsOld.mCode)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deliverydetails);
				audit.mField = "code";
				audit.mOldValue = deliverydetailsOld.mCode;
				audit.mNewValue = deliverydetails.mCode;
				audit_collection.Add(audit);
			}

			if (deliverydetails.mPlanned != deliverydetailsOld.mPlanned)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deliverydetails);
				audit.mField = "planned";
				audit.mOldValue = deliverydetailsOld.mPlanned.ToString();
				audit.mNewValue = deliverydetails.mPlanned.ToString();
				audit_collection.Add(audit);
			}

			if (deliverydetails.mAdditional != deliverydetailsOld.mAdditional)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deliverydetails);
				audit.mField = "additional";
				audit.mOldValue = deliverydetailsOld.mAdditional.ToString();
				audit.mNewValue = deliverydetails.mAdditional.ToString();
				audit_collection.Add(audit);
			}

			if (deliverydetails.mCancel != deliverydetailsOld.mCancel)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deliverydetails);
				audit.mField = "cancel";
				audit.mOldValue = deliverydetailsOld.mCancel.ToString();
				audit.mNewValue = deliverydetails.mCancel.ToString();
				audit_collection.Add(audit);
			}

			if (deliverydetails.mDdir != deliverydetailsOld.mDdir)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deliverydetails);
				audit.mField = "ddir";
				audit.mOldValue = deliverydetailsOld.mDdir.ToString();
				audit.mNewValue = deliverydetails.mDdir.ToString();
				audit_collection.Add(audit);
			}

			if (deliverydetails.mDatestamp != deliverydetailsOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deliverydetails);
				audit.mField = "datestamp";
				audit.mOldValue = deliverydetailsOld.mDatestamp.ToString();
				audit.mNewValue = deliverydetails.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, DeliveryDetails deliverydetails)
		{
			audit.mUserFullName = deliverydetails.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_DeliveryDetails);
			audit.mRowId = deliverydetails.mId;
			audit.mAction = 2;
		}
	}
}