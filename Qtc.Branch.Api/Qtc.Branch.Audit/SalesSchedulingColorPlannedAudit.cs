using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class SalesSchedulingColorPlannedAudit
	{
		public static AuditCollection Audit(SalesSchedulingColorPlanned salesschedulingcolorplanned,SalesSchedulingColorPlanned salesschedulingcolorplannedOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (salesschedulingcolorplanned.mOrderBy != salesschedulingcolorplannedOld.mOrderBy)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingcolorplanned);
				audit.mField = "order_by";
				audit.mOldValue = salesschedulingcolorplannedOld.mOrderBy.ToString();
				audit.mNewValue = salesschedulingcolorplanned.mOrderBy.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingcolorplanned.mName != salesschedulingcolorplannedOld.mName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingcolorplanned);
				audit.mField = "name";
				audit.mOldValue = salesschedulingcolorplannedOld.mName;
				audit.mNewValue = salesschedulingcolorplanned.mName;
				audit_collection.Add(audit);
			}

			if (salesschedulingcolorplanned.mRemarks != salesschedulingcolorplannedOld.mRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingcolorplanned);
				audit.mField = "remarks";
				audit.mOldValue = salesschedulingcolorplannedOld.mRemarks;
				audit.mNewValue = salesschedulingcolorplanned.mRemarks;
				audit_collection.Add(audit);
			}

			if (salesschedulingcolorplanned.mBackColor != salesschedulingcolorplannedOld.mBackColor)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingcolorplanned);
				audit.mField = "back_color";
				audit.mOldValue = salesschedulingcolorplannedOld.mBackColor;
				audit.mNewValue = salesschedulingcolorplanned.mBackColor;
				audit_collection.Add(audit);
			}

			if (salesschedulingcolorplanned.mForeColor != salesschedulingcolorplannedOld.mForeColor)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingcolorplanned);
				audit.mField = "fore_color";
				audit.mOldValue = salesschedulingcolorplannedOld.mForeColor;
				audit.mNewValue = salesschedulingcolorplanned.mForeColor;
				audit_collection.Add(audit);
			}

			if (salesschedulingcolorplanned.mRequiredRtwNo != salesschedulingcolorplannedOld.mRequiredRtwNo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingcolorplanned);
				audit.mField = "required_rtw_no";
				audit.mOldValue = salesschedulingcolorplannedOld.mRequiredRtwNo.ToString();
				audit.mNewValue = salesschedulingcolorplanned.mRequiredRtwNo.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingcolorplanned.mRequiredNov != salesschedulingcolorplannedOld.mRequiredNov)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingcolorplanned);
				audit.mField = "required_nov";
				audit.mOldValue = salesschedulingcolorplannedOld.mRequiredNov.ToString();
				audit.mNewValue = salesschedulingcolorplanned.mRequiredNov.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingcolorplanned.mIsWorking != salesschedulingcolorplannedOld.mIsWorking)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingcolorplanned);
				audit.mField = "is_working";
				audit.mOldValue = salesschedulingcolorplannedOld.mIsWorking.ToString();
				audit.mNewValue = salesschedulingcolorplanned.mIsWorking.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingcolorplanned.mManPowerCount != salesschedulingcolorplannedOld.mManPowerCount)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingcolorplanned);
				audit.mField = "man_power_count";
				audit.mOldValue = salesschedulingcolorplannedOld.mManPowerCount.ToString();
				audit.mNewValue = salesschedulingcolorplanned.mManPowerCount.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingcolorplanned.mIsUnlimited != salesschedulingcolorplannedOld.mIsUnlimited)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingcolorplanned);
				audit.mField = "is_unlimited";
				audit.mOldValue = salesschedulingcolorplannedOld.mIsUnlimited.ToString();
				audit.mNewValue = salesschedulingcolorplanned.mIsUnlimited.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingcolorplanned.mNumberOfUsePerWeek != salesschedulingcolorplannedOld.mNumberOfUsePerWeek)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingcolorplanned);
				audit.mField = "number_of_use_per_week";
				audit.mOldValue = salesschedulingcolorplannedOld.mNumberOfUsePerWeek.ToString();
				audit.mNewValue = salesschedulingcolorplanned.mNumberOfUsePerWeek.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingcolorplanned.mIsPaid != salesschedulingcolorplannedOld.mIsPaid)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingcolorplanned);
				audit.mField = "is_paid";
				audit.mOldValue = salesschedulingcolorplannedOld.mIsPaid.ToString();
				audit.mNewValue = salesschedulingcolorplanned.mIsPaid.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingcolorplanned.mBackColorHex != salesschedulingcolorplannedOld.mBackColorHex)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingcolorplanned);
				audit.mField = "back_color_hex";
				audit.mOldValue = salesschedulingcolorplannedOld.mBackColorHex;
				audit.mNewValue = salesschedulingcolorplanned.mBackColorHex;
				audit_collection.Add(audit);
			}

			if (salesschedulingcolorplanned.mForColorHex != salesschedulingcolorplannedOld.mForColorHex)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingcolorplanned);
				audit.mField = "for_color_hex";
				audit.mOldValue = salesschedulingcolorplannedOld.mForColorHex;
				audit.mNewValue = salesschedulingcolorplanned.mForColorHex;
				audit_collection.Add(audit);
			}

			if (salesschedulingcolorplanned.mIsActive != salesschedulingcolorplannedOld.mIsActive)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingcolorplanned);
				audit.mField = "is_active";
				audit.mOldValue = salesschedulingcolorplannedOld.mIsActive.ToString();
				audit.mNewValue = salesschedulingcolorplanned.mIsActive.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, SalesSchedulingColorPlanned salesschedulingcolorplanned)
		{
			audit.mUserFullName = salesschedulingcolorplanned.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_SalesSchedulingColorPlanned);
			audit.mRowId = salesschedulingcolorplanned.mId;
			audit.mAction = 2;
		}
	}
}