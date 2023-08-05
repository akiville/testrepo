using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class DisseminatedLetterAudit
	{
		public static AuditCollection Audit(DisseminatedLetter disseminatedletter,DisseminatedLetter disseminatedletterOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (disseminatedletter.mHrLetterId != disseminatedletterOld.mHrLetterId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, disseminatedletter);
				audit.mField = "hr_letter_id";
				audit.mOldValue = disseminatedletterOld.mHrLetterId.ToString();
				audit.mNewValue = disseminatedletter.mHrLetterId.ToString();
				audit_collection.Add(audit);
			}

			if (disseminatedletter.mLmm != disseminatedletterOld.mLmm)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, disseminatedletter);
				audit.mField = "lmm";
				audit.mOldValue = disseminatedletterOld.mLmm;
				audit.mNewValue = disseminatedletter.mLmm;
				audit_collection.Add(audit);
			}

			if (disseminatedletter.mDateCreated != disseminatedletterOld.mDateCreated)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, disseminatedletter);
				audit.mField = "date_created";
				audit.mOldValue = disseminatedletterOld.mDateCreated.ToString();
				audit.mNewValue = disseminatedletter.mDateCreated.ToString();
				audit_collection.Add(audit);
			}

			if (disseminatedletter.mAreaName != disseminatedletterOld.mAreaName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, disseminatedletter);
				audit.mField = "area_name";
				audit.mOldValue = disseminatedletterOld.mAreaName;
				audit.mNewValue = disseminatedletter.mAreaName;
				audit_collection.Add(audit);
			}

			if (disseminatedletter.mName != disseminatedletterOld.mName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, disseminatedletter);
				audit.mField = "name";
				audit.mOldValue = disseminatedletterOld.mName;
				audit.mNewValue = disseminatedletter.mName;
				audit_collection.Add(audit);
			}

			if (disseminatedletter.mAgencyIdName != disseminatedletterOld.mAgencyIdName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, disseminatedletter);
				audit.mField = "agency_id_name";
				audit.mOldValue = disseminatedletterOld.mAgencyIdName;
				audit.mNewValue = disseminatedletter.mAgencyIdName;
				audit_collection.Add(audit);
			}

			if (disseminatedletter.mBranch != disseminatedletterOld.mBranch)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, disseminatedletter);
				audit.mField = "branch";
				audit.mOldValue = disseminatedletterOld.mBranch;
				audit.mNewValue = disseminatedletter.mBranch;
				audit_collection.Add(audit);
			}

			if (disseminatedletter.mType != disseminatedletterOld.mType)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, disseminatedletter);
				audit.mField = "type";
				audit.mOldValue = disseminatedletterOld.mType;
				audit.mNewValue = disseminatedletter.mType;
				audit_collection.Add(audit);
			}

			if (disseminatedletter.mDuration != disseminatedletterOld.mDuration)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, disseminatedletter);
				audit.mField = "duration";
				audit.mOldValue = disseminatedletterOld.mDuration;
				audit.mNewValue = disseminatedletter.mDuration;
				audit_collection.Add(audit);
			}

			if (disseminatedletter.mControlNo != disseminatedletterOld.mControlNo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, disseminatedletter);
				audit.mField = "control_no";
				audit.mOldValue = disseminatedletterOld.mControlNo;
				audit.mNewValue = disseminatedletter.mControlNo;
				audit_collection.Add(audit);
			}

			if (disseminatedletter.mDateTrip != disseminatedletterOld.mDateTrip)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, disseminatedletter);
				audit.mField = "date_trip";
				audit.mOldValue = disseminatedletterOld.mDateTrip.ToString();
				audit.mNewValue = disseminatedletter.mDateTrip.ToString();
				audit_collection.Add(audit);
			}

			if (disseminatedletter.mCourierName != disseminatedletterOld.mCourierName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, disseminatedletter);
				audit.mField = "courier_name";
				audit.mOldValue = disseminatedletterOld.mCourierName;
				audit.mNewValue = disseminatedletter.mCourierName;
				audit_collection.Add(audit);
			}

			if (disseminatedletter.mLmmId != disseminatedletterOld.mLmmId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, disseminatedletter);
				audit.mField = "lmm_id";
				audit.mOldValue = disseminatedletterOld.mLmmId.ToString();
				audit.mNewValue = disseminatedletter.mLmmId.ToString();
				audit_collection.Add(audit);
			}

			if (disseminatedletter.mActualExpirationDate != disseminatedletterOld.mActualExpirationDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, disseminatedletter);
				audit.mField = "actual_expiration_date";
				audit.mOldValue = disseminatedletterOld.mActualExpirationDate.ToString();
				audit.mNewValue = disseminatedletter.mActualExpirationDate.ToString();
				audit_collection.Add(audit);
			}

			if (disseminatedletter.mResponseDate != disseminatedletterOld.mResponseDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, disseminatedletter);
				audit.mField = "response_date";
				audit.mOldValue = disseminatedletterOld.mResponseDate.ToString();
				audit.mNewValue = disseminatedletter.mResponseDate.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, DisseminatedLetter disseminatedletter)
		{
			audit.mUserFullName = disseminatedletter.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_DisseminatedLetter);
			audit.mRowId = disseminatedletter.mId;
			audit.mAction = 2;
		}
	}
}