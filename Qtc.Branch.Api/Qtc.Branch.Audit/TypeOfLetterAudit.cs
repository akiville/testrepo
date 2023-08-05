using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class TypeOfLetterAudit
	{
		public static AuditCollection Audit(TypeOfLetter typeofletter,TypeOfLetter typeofletterOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (typeofletter.mName != typeofletterOld.mName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, typeofletter);
				audit.mField = "name";
				audit.mOldValue = typeofletterOld.mName;
				audit.mNewValue = typeofletter.mName;
				audit_collection.Add(audit);
			}

			if (typeofletter.mRemarks != typeofletterOld.mRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, typeofletter);
				audit.mField = "remarks";
				audit.mOldValue = typeofletterOld.mRemarks;
				audit.mNewValue = typeofletter.mRemarks;
				audit_collection.Add(audit);
			}

			if (typeofletter.mCode != typeofletterOld.mCode)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, typeofletter);
				audit.mField = "code";
				audit.mOldValue = typeofletterOld.mCode;
				audit.mNewValue = typeofletter.mCode;
				audit_collection.Add(audit);
			}

			if (typeofletter.mSubmitRequirement != typeofletterOld.mSubmitRequirement)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, typeofletter);
				audit.mField = "submit_requirement";
				audit.mOldValue = typeofletterOld.mSubmitRequirement.ToString();
				audit.mNewValue = typeofletter.mSubmitRequirement.ToString();
				audit_collection.Add(audit);
			}

			if (typeofletter.mMonthDuration != typeofletterOld.mMonthDuration)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, typeofletter);
				audit.mField = "month_duration";
				audit.mOldValue = typeofletterOld.mMonthDuration.ToString();
				audit.mNewValue = typeofletter.mMonthDuration.ToString();
				audit_collection.Add(audit);
			}

			if (typeofletter.mStcForm != typeofletterOld.mStcForm)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, typeofletter);
				audit.mField = "stc_form";
				audit.mOldValue = typeofletterOld.mStcForm.ToString();
				audit.mNewValue = typeofletter.mStcForm.ToString();
				audit_collection.Add(audit);
			}

			if (typeofletter.mEgress != typeofletterOld.mEgress)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, typeofletter);
				audit.mField = "egress";
				audit.mOldValue = typeofletterOld.mEgress.ToString();
				audit.mNewValue = typeofletter.mEgress.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, TypeOfLetter typeofletter)
		{
			audit.mUserFullName = typeofletter.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_TypeOfLetter);
			audit.mRowId = typeofletter.mId;
			audit.mAction = 2;
		}
	}
}