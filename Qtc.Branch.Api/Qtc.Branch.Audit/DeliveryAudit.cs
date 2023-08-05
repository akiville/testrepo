using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class DeliveryAudit
	{
		public static AuditCollection Audit(Delivery delivery,Delivery deliveryOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (delivery.mDeliveryDate != deliveryOld.mDeliveryDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, delivery);
				audit.mField = "delivery_date";
				audit.mOldValue = deliveryOld.mDeliveryDate.ToString();
				audit.mNewValue = delivery.mDeliveryDate.ToString();
				audit_collection.Add(audit);
			}

			if (delivery.mPlannedBy != deliveryOld.mPlannedBy)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, delivery);
				audit.mField = "planned_by";
				audit.mOldValue = deliveryOld.mPlannedBy;
				audit.mNewValue = delivery.mPlannedBy;
				audit_collection.Add(audit);
			}

			if (delivery.mTrucking != deliveryOld.mTrucking)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, delivery);
				audit.mField = "trucking";
				audit.mOldValue = deliveryOld.mTrucking;
				audit.mNewValue = delivery.mTrucking;
				audit_collection.Add(audit);
			}

			if (delivery.mDriver != deliveryOld.mDriver)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, delivery);
				audit.mField = "driver";
				audit.mOldValue = deliveryOld.mDriver;
				audit.mNewValue = delivery.mDriver;
				audit_collection.Add(audit);
			}

			if (delivery.mCrew != deliveryOld.mCrew)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, delivery);
				audit.mField = "crew";
				audit.mOldValue = deliveryOld.mCrew;
				audit.mNewValue = delivery.mCrew;
				audit_collection.Add(audit);
			}

			if (delivery.mBranch != deliveryOld.mBranch)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, delivery);
				audit.mField = "branch";
				audit.mOldValue = deliveryOld.mBranch;
				audit.mNewValue = delivery.mBranch;
				audit_collection.Add(audit);
			}

			if (delivery.mBranchId != deliveryOld.mBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, delivery);
				audit.mField = "branch_id";
				audit.mOldValue = deliveryOld.mBranchId.ToString();
				audit.mNewValue = delivery.mBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (delivery.mDeliverySchedule != deliveryOld.mDeliverySchedule)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, delivery);
				audit.mField = "delivery_schedule";
				audit.mOldValue = deliveryOld.mDeliverySchedule;
				audit.mNewValue = delivery.mDeliverySchedule;
				audit_collection.Add(audit);
			}

			if (delivery.mEta != deliveryOld.mEta)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, delivery);
				audit.mField = "eta";
				audit.mOldValue = deliveryOld.mEta;
				audit.mNewValue = delivery.mEta;
				audit_collection.Add(audit);
			}

			if (delivery.mCrewToDrop != deliveryOld.mCrewToDrop)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, delivery);
				audit.mField = "crew_to_drop";
				audit.mOldValue = deliveryOld.mCrewToDrop;
				audit.mNewValue = delivery.mCrewToDrop;
				audit_collection.Add(audit);
			}

			if (delivery.mDatestamp != deliveryOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, delivery);
				audit.mField = "datestamp";
				audit.mOldValue = deliveryOld.mDatestamp.ToString();
				audit.mNewValue = delivery.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, Delivery delivery)
		{
			audit.mUserFullName = delivery.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_Delivery);
			audit.mRowId = delivery.mId;
			audit.mAction = 2;
		}
	}
}