using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class BranchsAudit
	{
		public static AuditCollection Audit(Branchs branchs,Branchs branchsOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (branchs.mBranchId != branchsOld.mBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchs);
				audit.mField = "branch_id";
				audit.mOldValue = branchsOld.mBranchId.ToString();
				audit.mNewValue = branchs.mBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (branchs.mCode != branchsOld.mCode)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchs);
				audit.mField = "code";
				audit.mOldValue = branchsOld.mCode;
				audit.mNewValue = branchs.mCode;
				audit_collection.Add(audit);
			}

			if (branchs.mName != branchsOld.mName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchs);
				audit.mField = "name";
				audit.mOldValue = branchsOld.mName;
				audit.mNewValue = branchs.mName;
				audit_collection.Add(audit);
			}

			if (branchs.mCode2 != branchsOld.mCode2)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchs);
				audit.mField = "code2";
				audit.mOldValue = branchsOld.mCode2;
				audit.mNewValue = branchs.mCode2;
				audit_collection.Add(audit);
			}

			if (branchs.mClassification != branchsOld.mClassification)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchs);
				audit.mField = "classification";
				audit.mOldValue = branchsOld.mClassification;
				audit.mNewValue = branchs.mClassification;
				audit_collection.Add(audit);
			}

			if (branchs.mCompany != branchsOld.mCompany)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchs);
				audit.mField = "company";
				audit.mOldValue = branchsOld.mCompany;
				audit.mNewValue = branchs.mCompany;
				audit_collection.Add(audit);
			}

			if (branchs.mTin != branchsOld.mTin)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchs);
				audit.mField = "tin";
				audit.mOldValue = branchsOld.mTin;
				audit.mNewValue = branchs.mTin;
				audit_collection.Add(audit);
			}

			if (branchs.mAreaId != branchsOld.mAreaId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchs);
				audit.mField = "area_id";
				audit.mOldValue = branchsOld.mAreaId.ToString();
				audit.mNewValue = branchs.mAreaId.ToString();
				audit_collection.Add(audit);
			}

			if (branchs.mAddress != branchsOld.mAddress)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchs);
				audit.mField = "address";
				audit.mOldValue = branchsOld.mAddress;
				audit.mNewValue = branchs.mAddress;
				audit_collection.Add(audit);
			}

			if (branchs.mMcId != branchsOld.mMcId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchs);
				audit.mField = "mc_id";
				audit.mOldValue = branchsOld.mMcId.ToString();
				audit.mNewValue = branchs.mMcId.ToString();
				audit_collection.Add(audit);
			}

			if (branchs.mBank != branchsOld.mBank)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchs);
				audit.mField = "bank";
				audit.mOldValue = branchsOld.mBank;
				audit.mNewValue = branchs.mBank;
				audit_collection.Add(audit);
			}

			if (branchs.mContactNo != branchsOld.mContactNo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchs);
				audit.mField = "contact_no";
				audit.mOldValue = branchsOld.mContactNo;
				audit.mNewValue = branchs.mContactNo;
				audit_collection.Add(audit);
			}

			if (branchs.mStoreHourStart != branchsOld.mStoreHourStart)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchs);
				audit.mField = "store_hour_start";
				audit.mOldValue = branchsOld.mStoreHourStart;
				audit.mNewValue = branchs.mStoreHourStart;
				audit_collection.Add(audit);
			}

			if (branchs.mStoreHourEnd != branchsOld.mStoreHourEnd)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchs);
				audit.mField = "store_hour_end";
				audit.mOldValue = branchsOld.mStoreHourEnd;
				audit.mNewValue = branchs.mStoreHourEnd;
				audit_collection.Add(audit);
			}

			if (branchs.mDeliveryTime != branchsOld.mDeliveryTime)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchs);
				audit.mField = "delivery_time";
				audit.mOldValue = branchsOld.mDeliveryTime.ToString();
				audit.mNewValue = branchs.mDeliveryTime.ToString();
				audit_collection.Add(audit);
			}

			if (branchs.mSunday != branchsOld.mSunday)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchs);
				audit.mField = "sunday";
				audit.mOldValue = branchsOld.mSunday.ToString();
				audit.mNewValue = branchs.mSunday.ToString();
				audit_collection.Add(audit);
			}

			if (branchs.mMonday != branchsOld.mMonday)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchs);
				audit.mField = "monday";
				audit.mOldValue = branchsOld.mMonday.ToString();
				audit.mNewValue = branchs.mMonday.ToString();
				audit_collection.Add(audit);
			}

			if (branchs.mTuesday != branchsOld.mTuesday)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchs);
				audit.mField = "tuesday";
				audit.mOldValue = branchsOld.mTuesday.ToString();
				audit.mNewValue = branchs.mTuesday.ToString();
				audit_collection.Add(audit);
			}

			if (branchs.mWednesday != branchsOld.mWednesday)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchs);
				audit.mField = "wednesday";
				audit.mOldValue = branchsOld.mWednesday.ToString();
				audit.mNewValue = branchs.mWednesday.ToString();
				audit_collection.Add(audit);
			}

			if (branchs.mThursday != branchsOld.mThursday)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchs);
				audit.mField = "thursday";
				audit.mOldValue = branchsOld.mThursday.ToString();
				audit.mNewValue = branchs.mThursday.ToString();
				audit_collection.Add(audit);
			}

			if (branchs.mFriday != branchsOld.mFriday)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchs);
				audit.mField = "friday";
				audit.mOldValue = branchsOld.mFriday.ToString();
				audit.mNewValue = branchs.mFriday.ToString();
				audit_collection.Add(audit);
			}

			if (branchs.mSaturday != branchsOld.mSaturday)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchs);
				audit.mField = "saturday";
				audit.mOldValue = branchsOld.mSaturday.ToString();
				audit.mNewValue = branchs.mSaturday.ToString();
				audit_collection.Add(audit);
			}

			if (branchs.mBankId != branchsOld.mBankId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchs);
				audit.mField = "bank_id";
				audit.mOldValue = branchsOld.mBankId.ToString();
				audit.mNewValue = branchs.mBankId.ToString();
				audit_collection.Add(audit);
			}

			if (branchs.mRpAssistantId != branchsOld.mRpAssistantId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchs);
				audit.mField = "rp_assistant_id";
				audit.mOldValue = branchsOld.mRpAssistantId.ToString();
				audit.mNewValue = branchs.mRpAssistantId.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, Branchs branchs)
		{
			audit.mUserFullName = branchs.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_Branchs);
			audit.mRowId = branchs.mId;
			audit.mAction = 2;
		}
	}
}