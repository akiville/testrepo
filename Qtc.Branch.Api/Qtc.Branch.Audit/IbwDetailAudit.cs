using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class IbwDetailAudit
	{
		public static AuditCollection Audit(IbwDetail ibwdetail,IbwDetail ibwdetailOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (ibwdetail.mIbwId != ibwdetailOld.mIbwId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, ibwdetail);
				audit.mField = "ibw_id";
				audit.mOldValue = ibwdetailOld.mIbwId.ToString();
				audit.mNewValue = ibwdetail.mIbwId.ToString();
				audit_collection.Add(audit);
			}

			if (ibwdetail.mProductId != ibwdetailOld.mProductId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, ibwdetail);
				audit.mField = "product_id";
				audit.mOldValue = ibwdetailOld.mProductId.ToString();
				audit.mNewValue = ibwdetail.mProductId.ToString();
				audit_collection.Add(audit);
			}

			if (ibwdetail.mQuantity != ibwdetailOld.mQuantity)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, ibwdetail);
				audit.mField = "quantity";
				audit.mOldValue = ibwdetailOld.mQuantity.ToString();
				audit.mNewValue = ibwdetail.mQuantity.ToString();
				audit_collection.Add(audit);
			}

			if (ibwdetail.mCheckedQuantity != ibwdetailOld.mCheckedQuantity)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, ibwdetail);
				audit.mField = "checked_quantity";
				audit.mOldValue = ibwdetailOld.mCheckedQuantity.ToString();
				audit.mNewValue = ibwdetail.mCheckedQuantity.ToString();
				audit_collection.Add(audit);
			}

			if (ibwdetail.mNovNo != ibwdetailOld.mNovNo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, ibwdetail);
				audit.mField = "nov_no";
				audit.mOldValue = ibwdetailOld.mNovNo;
				audit.mNewValue = ibwdetail.mNovNo;
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, IbwDetail ibwdetail)
		{
			audit.mUserFullName = ibwdetail.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_IbwDetail);
			audit.mRowId = ibwdetail.mId;
			audit.mAction = 2;
		}
	}
}